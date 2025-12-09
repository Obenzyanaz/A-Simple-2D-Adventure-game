using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameController gameController;
    public Transform RespawnPoint;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("player").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            gameController.UpdateCheckpoint(RespawnPoint.position);
        }
    }
}
