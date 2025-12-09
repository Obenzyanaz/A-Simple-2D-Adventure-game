using UnityEngine;
using UnityEngine.UI;
                      
                     
   public class NPCMenuManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject shopPanel;
    public GameObject savePanel;
    public GameObject askPanel;

    public void OpenShop()
    {
        CloseAllMenus();
        shopPanel.SetActive(true);
    }

    public void OpenDialogue()
    {
        CloseAllMenus();
        dialoguePanel.SetActive(true);
    }

    public void OpenSave()
    {
        CloseAllMenus();
        savePanel.SetActive(true);
    }

    public void OpenAsk()
    {
        CloseAllMenus();
        askPanel.SetActive(true);
    }

    public void ExitConversation()
    {
        CloseAllMenus(); // hides everything
    }

    private void CloseAllMenus()
    {
        dialoguePanel.SetActive(false);
        shopPanel.SetActive(false);
        savePanel.SetActive(false);
        askPanel.SetActive(false);
    }
}