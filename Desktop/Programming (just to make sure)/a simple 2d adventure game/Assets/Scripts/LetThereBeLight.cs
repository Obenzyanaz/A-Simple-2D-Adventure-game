using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CastleLightZone : MonoBehaviour
{
    public Light2D outsideLight; // bright sunlight
    public Light2D insideLight;  // castle interior light

    [Header("Desired Intensities")]
    public float outsideLightOutside = 1f;  // outside castle
    public float outsideLightInside = 0.2f; // outside light when inside castle
    public float insideLightOutside = 0f;   // inside castle when outside
    public float insideLightInside = 0.5f;  // inside castle when inside

    public float transitionSpeed = 2f; // speed of fade

    private bool insideCastle = false;

    void Update()
    {
        // Smoothly interpolate to the target intensity
        float targetOutside = insideCastle ? outsideLightInside : outsideLightOutside;
        float targetInside = insideCastle ? insideLightInside : insideLightOutside;

        outsideLight.intensity = Mathf.Lerp(outsideLight.intensity, targetOutside, Time.deltaTime * transitionSpeed);
        insideLight.intensity = Mathf.Lerp(insideLight.intensity, targetInside, Time.deltaTime * transitionSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player")) insideCastle = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("player")) insideCastle = false;
    }
}