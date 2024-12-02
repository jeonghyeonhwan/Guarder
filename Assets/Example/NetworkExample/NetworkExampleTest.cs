using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class NetworkExampleTest : MonoBehaviour
{
	public GameObject TopPanel;
	public GameObject MiddlePanel;
	public InputField ValueInputField;
	public GameObject BottomPanel;

	public Text StatusMessageText;

	public InputField DeviceName;

	public class Characteristic
	{
		public string ServiceUUID;
		public string CharacteristicUUID;
		public bool Found;
	}

	public static List<Characteristic> Characteristics = new List<Characteristic>
	{
		new Characteristic { ServiceUUID = "37190001-7638-4216-B629-96AD40F79AA1", CharacteristicUUID = "37190002-7638-4216-B629-96AD40F79AA1", Found = false },
	};

	public Characteristic SampleCharacteristic = Characteristics[0];

	public bool AllCharacteristicsFound { get { return !(Characteristics.Where (c => c.Found == false).Any ()); } }
	public Characteristic GetCharacteristic (string serviceUUID, string characteristicsUUID)
	{
		return Characteristics.Where (c => IsEqual (serviceUUID, c.ServiceUUID) && IsEqual (characteristicsUUID, c.CharacteristicUUID)).FirstOrDefault ();
	}

	enum States
	{
		None,
		Scan,
		Connect,
		Subscribe,
		Disconnect,
		Disconnecting,
	}

	private bool _connected = false;
	private float _timeout = 0f;
	private States _state = States.None;
	private string _deviceAddress;
	private bool _isCentral = true;

	string StatusMessage
	{
		set
		{
			if (!string.IsNullOrEmpty(value))
				BluetoothLEHardwareInterface.Log (value);
			if (StatusMessageText != null)
				StatusMessageText.text = value;
		}
	}

	public void OnButton (Button button)
	{
		if (button.name.Equals ("ButtonCentral"))
		{
			_isCentral = true;
			BottomPanel.SetActive (false);
			SetState (States.Scan, 0.5f);
		}
		else if (button.name.Equals ("ButtonPeripheral"))
		{
			_isCentral = false;
			BottomPanel.SetActive (false);

			BluetoothLEHardwareInterface.PeripheralName (DeviceName.text);

			BluetoothLEHardwareInterface.RemoveServices ();
			BluetoothLEHardwareInterface.RemoveCharacteristics ();

			BluetoothLEHardwareInterface.CBCharacteristicProperties properties =
				BluetoothLEHardwareInterface.CBCharacteristicProperties.CBCharacteristicPropertyRead |
				BluetoothLEHardwareInterface.CBCharacteristicProperties.CBCharacteristicPropertyWrite |
				BluetoothLEHardwareInterface.CBCharacteristicProperties.CBCharacteristicPropertyNotify;

			BluetoothLEHardwareInterface.CBAttributePermissions permissions =
				BluetoothLEHardwareInterface.CBAttributePermissions.CBAttributePermissionsReadable |
				BluetoothLEHardwareInterface.CBAttributePermissions.CBAttributePermissionsWriteable;

			BluetoothLEHardwareInterface.CreateCharacteristic (SampleCharacteristic.CharacteristicUUID, properties, permissions, null, 5, (characteristicWritten, bytes) => {
				ValueInputField.text = Encoding.UTF8.GetString (bytes);
			});

			BluetoothLEHardwareInterface.CreateService (SampleCharacteristic.ServiceUUID, true, (characteristic) => {
				StatusMessage = "Created service";
			});

			BluetoothLEHardwareInterface.StartAdvertising (() => {
				MiddlePanel.SetActive (true);
			});
		}
		else if (button.name.Equals ("ButtonSend"))
		{
			if (_isCentral)
			{
				BluetoothLEHardwareInterface.WriteCharacteristic (_deviceAddress, SampleCharacteristic.ServiceUUID, SampleCharacteristic.CharacteristicUUID, Encoding.UTF8.GetBytes (ValueInputField.text), ValueInputField.text.Length, true, (characteristicWrite) => {

				});
			}
			else
			{
				BluetoothLEHardwareInterface.UpdateCharacteristicValue (SampleCharacteristic.CharacteristicUUID, Encoding.UTF8.GetBytes (ValueInputField.text), ValueInputField.text.Length);
			}
		}
		else if (button.name.Equals("ButtonStop"))
		{
			if (_isCentral)
			{
				SetState (States.Disconnect, 0.5f);
			}
			else
			{
				BluetoothLEHardwareInterface.StopAdvertising (() => {
					Reset ();
				});
			}
		}
	}

	void Reset ()
	{
		_connected = false;
		_timeout = 0f;
		_state = States.None;
		_deviceAddress = null;
		_isCentral = true;
		TopPanel.SetActive (true);
		MiddlePanel.SetActive (false);
		BottomPanel.SetActive (true);

		StatusMessage = "";
	}

	void SetState (States newState, float timeout)
	{
		_state = newState;
		_timeout = timeout;
	}

	void StartProcess ()
	{
		Reset ();
		BluetoothLEHardwareInterface.Initialize (true, true, () => {

		}, (error) => {

			var message = "Error: " + error;
			StatusMessage = message;
			BluetoothLEHardwareInterface.Log (message);
		});
	}

	// Use this for initialization
	void Start ()
	{
		StartProcess ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (_timeout > 0f)
		{
			_timeout -= Time.deltaTime;
			if (_timeout <= 0f)
			{
				_timeout = 0f;

				switch (_state)
				{
				case States.None:
					break;

				case States.Scan:
					BluetoothLEHardwareInterface.ScanForPeripheralsWithServices (null, (address, deviceName) => {
						
						if (deviceName.Contains (DeviceName.text))
						{
							StatusMessage = "Found the other device";

							BluetoothLEHardwareInterface.StopScan ();

							// found a device with the name we want
							// this example does not deal with finding more than one
							_deviceAddress = address;
							SetState (States.Connect, 0.5f);
						}

					}, null, true);
					break;

				case States.Connect:
					StatusMessage = "Connecting to other device...";

					BluetoothLEHardwareInterface.ConnectToPeripheral (_deviceAddress, null, null, (address, serviceUUID, characteristicUUID) => {

						var characteristic = GetCharacteristic (serviceUUID, characteristicUUID);
						if (characteristic != null)
						{
							BluetoothLEHardwareInterface.Log (string.Format ("Found {0}, {1}", serviceUUID, characteristicUUID));

							characteristic.Found = true;

							if (AllCharacteristicsFound)
							{
								_connected = true;
								MiddlePanel.SetActive (true);
								SetState (States.Subscribe, 3f);
							}
						}
					}, (disconnectAddress) => {
						StatusMessage = "Disconnected from other device";
						Reset ();
					});
					break;

				case States.Subscribe:
					_state = States.None;
					StatusMessage = "Subscribe to device";
					BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress (_deviceAddress, SampleCharacteristic.ServiceUUID, SampleCharacteristic.CharacteristicUUID, null, (deviceAddress, characteristric, bytes) => {

						MiddlePanel.SetActive (true);

						ValueInputField.text = Encoding.UTF8.GetString (bytes);
					});
					break;

				case States.Disconnect:
					SetState (States.Disconnecting, 5f);
					if (_connected)
					{
						BluetoothLEHardwareInterface.DisconnectPeripheral (_deviceAddress, (address) => {
							// since we have a callback for disconnect in the connect method above, we don't
							// need to process the callback here.
						});
					}
					else
					{
						Reset ();
					}
					break;

				case States.Disconnecting:
					// if we got here we timed out disconnecting, so just go to disconnected state
					Reset ();
					break;
				}
			}
		}
	}

	bool IsEqual (string uuid1, string uuid2)
	{
		return (uuid1.ToUpper ().CompareTo (uuid2.ToUpper ()) == 0);
	}
}
