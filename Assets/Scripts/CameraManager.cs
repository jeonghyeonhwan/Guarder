// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.Android;

// public class CameraManager : MonoBehaviour
// {
//     public RawImage displayImage;
//     private WebCamTexture webCamTexture;
//     private bool isCameraInitialized = false;

//     void Start()
//     {
//         #if PLATFORM_ANDROID
//             if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
//             {
//                 Permission.RequestUserPermission(Permission.Camera);
//             }
//             else
//             {
//                 InitializeCamera();
//             }
//         #else
//             InitializeCamera();
//         #endif
//     }

//     void InitializeCamera()
//     {
//         Debug.Log("카메라 초기화 시작");
        
//         if (WebCamTexture.devices.Length == 0)
//         {
//             Debug.LogError("카메라를 찾을 수 없습니다!");
//             return;
//         }
        
//         Debug.Log($"발견된 카메라 개수: {WebCamTexture.devices.Length}");
//         foreach (var device in WebCamTexture.devices)
//         {
//             Debug.Log($"카메라 이름: {device.name}, 전면 카메라: {device.isFrontFacing}");
//         }

//         WebCamDevice? backCamera = null;
//         foreach (WebCamDevice device in WebCamTexture.devices)
//         {
//             if (!device.isFrontFacing)
//             {
//                 backCamera = device;
//                 break;
//             }
//         }

//         string deviceName = backCamera?.name ?? WebCamTexture.devices[0].name;
//         Debug.Log($"선택된 카메라: {deviceName}");
        
//         webCamTexture = new WebCamTexture(deviceName, 1280, 720, 30);
        
//         if (displayImage == null)
//         {
//             Debug.LogError("RawImage가 할당되지 않았습니다!");
//             return;
//         }
        
//         displayImage.texture = webCamTexture;
//         webCamTexture.Play();
        
//         Debug.Log($"카메라 실행 상태: {webCamTexture.isPlaying}");
        
//         isCameraInitialized = true;
        
//         displayImage.rectTransform.localEulerAngles = new Vector3(0, 0, -webCamTexture.videoRotationAngle);
//     }

//     void OnEnable()
//     {
//         if (isCameraInitialized && webCamTexture != null && !webCamTexture.isPlaying)
//         {
//             webCamTexture.Play();
//         }
//     }

//     void OnDisable()
//     {
//         if (webCamTexture != null && webCamTexture.isPlaying)
//         {
//             webCamTexture.Stop();
//         }
//     }

//     void OnApplicationPause(bool pause)
//     {
//         if (webCamTexture != null)
//         {
//             if (pause)
//             {
//                 webCamTexture.Stop();
//             }
//             else if (isCameraInitialized)
//             {
//                 webCamTexture.Play();
//             }
//         }
//     }

//     void OnDestroy()
//     {
//         if (webCamTexture != null)
//         {
//             webCamTexture.Stop();
//             Destroy(webCamTexture);
//         }
//     }

//     private void OnApplicationFocus(bool focus)
//     {
//         if (webCamTexture != null)
//         {
//             if (!focus)
//             {
//                 webCamTexture.Stop();
//             }
//             else if (isCameraInitialized)
//             {
//                 webCamTexture.Play();
//             }
//         }
//     }
// }