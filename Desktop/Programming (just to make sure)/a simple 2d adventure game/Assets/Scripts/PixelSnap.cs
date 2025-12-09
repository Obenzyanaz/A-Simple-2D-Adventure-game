using UnityEngine;

public class PixelSnap : MonoBehaviour
{
    public float pixelsPerUnit = 100f;

    void LateUpdate()
    {
        float snapValue = 1f / pixelsPerUnit;
        transform.position = new Vector3(
            Mathf.Round(transform.position.x / snapValue) * snapValue,
            Mathf.Round(transform.position.y / snapValue) * snapValue,
            transform.position.z
        );
    }
}