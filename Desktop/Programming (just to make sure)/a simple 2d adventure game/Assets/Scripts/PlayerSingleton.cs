using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    public static PlayerSingleton instance; // Global reference to this player

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Set the first one
            DontDestroyOnLoad(gameObject); // Make it persist between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }
}