using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public GameObject interactionUI; // Full UI menu
    private bool isPlayerNearby = false;

    // These should be dragged in Inspector
    public GameObject dialoguePanel;
    public GameObject shopPanel;
    public GameObject savePanel;
    public GameObject askPanel;

    void Update()
    {
        // Open dialogue menu with E
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            MusicManager.instance.PlayshopMusic();
            interactionUI.SetActive(true);
            dialoguePanel.SetActive(true); // Show menu buttons
            CloseAllMenus(); // Hide panels first
            Time.timeScale = 0f; // Pause game
        }
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC key closes everything
        {
            CloseAllMenus();
            interactionUI.SetActive(false);
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            isPlayerNearby = false;
        }
    }

    // Closes all submenus
    private void CloseAllMenus()
    {
        shopPanel.SetActive(false);
        savePanel.SetActive(false);
        askPanel.SetActive(false);
    }

    // Buttons ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

    public void OnShopButtonClicked()
    {
        CloseAllMenus();
        MusicManager.instance.PlayshopMusic(); // 🎵 Changes the music
        shopPanel.SetActive(true);
        interactionUI.SetActive(false);
    }

    public void CloseShop()
    {
        MusicManager.instance.PlayMusic(MusicManager.instance.defaultMusic); // 🔁 Switch back to default
        shopPanel.SetActive(false);
        interactionUI.SetActive(true);
    }

    public void OnSaveButtonClicked()
    {
        CloseAllMenus();
        savePanel.SetActive(true);
        interactionUI.SetActive(false);
    }

    public void OnAskButtonClicked()
    {
        CloseAllMenus();
        askPanel.SetActive(true);
        interactionUI.SetActive(false);
    }

    public void OnExitButtonClicked()
    {
        CloseAllMenus();
        dialoguePanel.SetActive(false); // Hide main menu buttons
        interactionUI.SetActive(false); // Hide full UI
        Time.timeScale = 1f; // Resume game
    }
    void Start()
    {
        CloseAllMenus();
        dialoguePanel.SetActive(false);
        interactionUI.SetActive(false);
    }

    public void ExitConversation()
    {
        interactionUI.SetActive(false); // hides the UI again
        MusicManager.instance.PlayMusic(MusicManager.instance.defaultMusic); // go back to default
        Time.timeScale = 1f;
    }
}
