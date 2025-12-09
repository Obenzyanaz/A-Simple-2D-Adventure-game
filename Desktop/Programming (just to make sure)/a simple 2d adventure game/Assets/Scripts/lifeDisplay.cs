using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    public Image digit1; // Main digit (used for 0–9)
    public Image digit2; // Optional second digit (used for 10–99)
    public Sprite[] numberSprites; // Array of digit sprites (0–9)

    public void UpdateDisplay(int lives)
    {
        if (lives < 10)
        {
            digit1.sprite = numberSprites[lives];
            digit2.enabled = false;
        }
        else
        {
            int tens = lives / 10;
            int ones = lives % 10;
            digit1.sprite = numberSprites[tens]; 
            digit2.sprite = numberSprites[ones];
            digit2.enabled = true;
        }
    }
}
