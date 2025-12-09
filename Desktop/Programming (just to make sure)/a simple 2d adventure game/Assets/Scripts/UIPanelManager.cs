using UnityEngine;

public class UIPanelManager : MonoBehaviour
{
    // Panels
    public GameObject descriptionPanel;
    public GameObject settingsPanel;
    public GameObject lorePanel;
    public GameObject berserkerPanel;

    // Character face
    public SpriteRenderer characterFace;
    public Sprite faceDamn;
    public Sprite faceLmao;
    public Sprite faceFunny;
    public Sprite faceEyesClosed;
    public Sprite faceSmiling;
    public Sprite faceDetermined;
    public Sprite faceSad;

    // Switch panels and optionally change face
    public void ShowPanel(string panelName)
    {
        // Enable only the requested panel
        descriptionPanel.SetActive(panelName == "Description");
        settingsPanel.SetActive(panelName == "Settings");
        lorePanel.SetActive(panelName == "Lore");
        berserkerPanel.SetActive(panelName == "Berserker");

        // Change character face based on panel
        switch (panelName)
        {
            case "Description":
                characterFace.sprite = faceSmiling;
                break;
            case "Settings":
                characterFace.sprite = faceDamn;       // example face
                break;
            case "Lore":
                characterFace.sprite = faceLmao; // if you have a description panel
                break;
            case "Berserker":
                characterFace.sprite = faceDetermined;
                break;
        }
    }
}