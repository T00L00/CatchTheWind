using UnityEngine;

public class Utils : MonoBehaviour
{
    // Get world position from screen position
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }
}
