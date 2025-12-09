using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeNPC : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject interactionUI;       // UI with 4 buttons
    public GameObject shopUI;              // Actual Shop UI Panel
    public FadeController fadeController;  // Handles fade image
    public float fadeDuration = 1f;

    [Header("UI Buttons")]
    public Button shopButton;              // Button inside interaction UI

    private bool isNearNPC = false;

    void Start()
    {
        // Hook up Shop button to open actual Shop panel
        if (shopButton != null)
        {
            shopButton.onClick.AddListener(ShowShopUI);
        }
    }

    void Update()
    {
        if (isNearNPC && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(FadeAndShowInteractionUI());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isNearNPC = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isNearNPC = false;
        }
    }

    // Fade in and show the main interaction UI (with 4 options)
    private IEnumerator FadeAndShowInteractionUI()
    {
        yield return StartCoroutine(fadeController.FadeOut(fadeDuration));

        interactionUI.SetActive(true); // Show the 4-option menu

        yield return StartCoroutine(fadeController.FadeIn(fadeDuration));
    }

    // Called when Shop button is clicked
    private void ShowShopUI()
    {
        interactionUI.SetActive(false);  // Hide the interaction menu
        shopUI.SetActive(true);          // Show the Shop panel
    }
}
