using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public GameObject shopUI;                  // Reference to the entire shop UI panel
    public Button dashButton;                  // Button to buy Dash ability
    public Button doubleJumpButton;            // Button to buy Double Jump
   
    private bool hasDash = false;              // Track if Dash is already bought
    private bool hasDoubleJump = false;        // Track if Double Jump is already bought

    void Start()
    {
        shopUI.SetActive(false);               // Hide shop at game start

        // Add listeners to buttons
        dashButton.onClick.AddListener(BuyDash);
        doubleJumpButton.onClick.AddListener(BuyDoubleJump);

        // Check saved data (we'll cover this next)
        LoadShopState();
    }
   
    public void OpenShop()
    {
        shopUI.SetActive(true);                // Show shop panel
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);               // Hide shop panel
    }

    public void BuyDash()
    {
        if (!hasDash)
        {
            hasDash = true;                    // Mark Dash as bought
            dashButton.interactable = false;   // Disable the Dash button
            Debug.Log("Dash Purchased!");
            SaveShopState();                   // Save progress
        }
    }

    public void BuyDoubleJump()
    {
        if (!hasDoubleJump)
        {
            hasDoubleJump = true;                   // Mark Double Jump as bought
            doubleJumpButton.interactable = false;  // Disable the button
            Debug.Log("Double Jump Purchased!");
            SaveShopState();                        // Save progress
        }
    }

    // 🔐 Save what has been purchased
    private void SaveShopState()
    {
        PlayerPrefs.SetInt("HasDash", hasDash ? 1 : 0);
        PlayerPrefs.SetInt("HasDoubleJump", hasDoubleJump ? 1 : 0);
    }

    // 🔄 Load saved data on start
    private void LoadShopState()
    {
        hasDash = PlayerPrefs.GetInt("HasDash", 0) == 1;
        hasDoubleJump = PlayerPrefs.GetInt("HasDoubleJump", 0) == 1;

        if (hasDash)
        {
            dashButton.interactable = false; // Disable button if already bought
        }

        if (hasDoubleJump)
        {
            doubleJumpButton.interactable = false; // Same here
        }
    }
}