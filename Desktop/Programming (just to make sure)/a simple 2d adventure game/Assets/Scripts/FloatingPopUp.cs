using UnityEngine;

public class FloatingPopUp : MonoBehaviour
{
    public float amplitude = 0.5f; // How high and low it floats
    public float frequency = 1f;   // How fast it floats

    private Vector3 startPos;      // Original position of the object

    void Start()
    {
        // Remember the original position
        startPos = transform.position;
    }

    void Update()
    {
        // Calculate a vertical offset using sine wave
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        // Apply the offset to the object's position
        transform.position = startPos + new Vector3(0, yOffset, 0);
    }
    public void ResetPosition()
    {
        transform.position = startPos;
    }
}