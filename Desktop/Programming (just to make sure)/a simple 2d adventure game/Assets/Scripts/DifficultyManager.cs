using UnityEngine;

public enum GameDifficulty
{
    Easy,
    Normal,
    Hard,
    Impossible
}

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager instance;

    public GameDifficulty selectedDifficulty;

    // Gameplay values based on difficulty
    public int startingLives = 3;
    public float enemySpeedMultiplier = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDifficulty(int difficultyIndex)
    {
        selectedDifficulty = (GameDifficulty)difficultyIndex;
        Debug.Log("Difficulty set to: " + selectedDifficulty);
        ApplyDifficultySettings();
    }

    // Sets gameplay values based on the difficulty
    void ApplyDifficultySettings()
    {
        switch (selectedDifficulty)
        {
            case GameDifficulty.Easy:
                startingLives = 10;
                enemySpeedMultiplier = 0.75f;
                break;
            case GameDifficulty.Normal:
                startingLives = 5;
                enemySpeedMultiplier = 1f;
                break;
            case GameDifficulty.Hard:
                startingLives = 3;
                enemySpeedMultiplier = 1.25f;
                break;
            case GameDifficulty.Impossible:
                startingLives = 1;
                enemySpeedMultiplier = 1.5f;
                break;
        }
    }
}
