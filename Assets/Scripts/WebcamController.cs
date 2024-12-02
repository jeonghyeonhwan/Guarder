using UnityEngine;
using UnityEngine.UI;

public class WebcamController : MonoBehaviour
{
    public RawImage display;
    private WebCamTexture webcamTexture;

    public void StartCamera()
    {
        if (webcamTexture == null)
        {
            webcamTexture = new WebCamTexture();
        }
        display.texture = webcamTexture;
        webcamTexture.Play();
    }

    public void StopCamera()
    {
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            webcamTexture.Stop();
        }
    }
}