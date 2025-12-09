using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public float parallaxFactor = 0.2f; // How fast background moves horizontally

    private Transform player;
    private float lastPlayerX;

    void Start()
    {
        // Find the player in the scene at runtime
        player = GameObject.FindGameObjectWithTag("player").transform;

        if (player != null)
            lastPlayerX = player.position.x;
        else
            Debug.LogWarning("Player not found! Make sure it has the 'player' tag.");
    }

    void Update()
    {
        if (player == null) return; // If player not found, do nothing

        float deltaX = player.position.x - lastPlayerX;
        transform.position += new Vector3(deltaX * parallaxFactor, 0, 0); // Horizontal only
        lastPlayerX = player.position.x;
    }
}