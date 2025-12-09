using UnityEngine;
using UnityEngine.Rendering.Universal; // for Light2D
using System.Collections.Generic; // Needed for List

public class LightSwitch : MonoBehaviour
{
    public List<Light2D> lights = new List<Light2D>(); // List to hold all lights
    public float lightOnIntensity = 1f;                // How bright the room gets
    public GameObject popupText;                        // The (E) or "Press E" popup
    private bool playerNear = false;                    // Tracks if player is close

    void Start()
    {
        // Make sure the popup starts hidden
        if (popupText != null)
            popupText.SetActive(false);
    }

    void Update()
    {
        // If player is near and presses E
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Turn all lights on
            foreach (Light2D light in lights)
            {
                if (light != null)
                    light.intensity = lightOnIntensity;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            playerNear = true;

            if (popupText != null)
                popupText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            playerNear = false;

            if (popupText != null)
                popupText.SetActive(false);
        }
    }
}