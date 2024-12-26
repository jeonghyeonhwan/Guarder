using UnityEngine;

public class NativeCameraController : MonoBehaviour
{
    public void OpenNativeCamera()
    {
        using (var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (AndroidJavaObject currentActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity"))
        {
            using (var intentObject = new AndroidJavaObject("android.content.Intent", "android.media.action.IMAGE_CAPTURE"))
            {
                currentActivity.Call("startActivityForResult", intentObject, 100);
            }
        }
    }
}