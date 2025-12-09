using UnityEngine;
using Cinemachine;

public class VirtualCameraSetter : MonoBehaviour
{
    private void Start()
    {
        // Try to find the player with the "Player" tag
        GameObject player = GameObject.FindGameObjectWithTag("player");

        // Get the virtual camera component
        CinemachineVirtualCamera virtualCam = GetComponent<CinemachineVirtualCamera>();

        if (player != null && virtualCam != null)
        {
            // Set the player as the camera's follow target
            virtualCam.Follow = player.transform;
            virtualCam.LookAt = player.transform; // optional: for aiming the camera toward the player
        }
    }
}