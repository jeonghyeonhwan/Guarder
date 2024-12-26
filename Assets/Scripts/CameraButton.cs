using UnityEngine;

public class CameraButton : MonoBehaviour
{
    public void OpenCamera()
    {
        GetComponent<NativeCameraController>().OpenNativeCamera();
    }
}