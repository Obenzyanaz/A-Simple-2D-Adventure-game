using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public Image sonicIcon;
    public Image digitImage;
    public Sprite[] numberSprites; // 0-9 sprites in array, set in Inspector

    public void UpdateLives(int lives)
    {
        if (lives < 0 || lives > 9) return;
        digitImage.sprite = numberSprites[lives];
    }
}