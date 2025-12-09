using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
        // Look for a GameObject tagged "SpawnPoint" in the scene
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");

        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
        else
        {
            Debug.LogWarning("No SpawnPoint found in the scene.");
        }
    }
}
