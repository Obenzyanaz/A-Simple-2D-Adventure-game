using UnityEngine;

public class CastleTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            MusicManager.instance.PlayMusic(MusicManager.instance.ArnellMusic);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            MusicManager.instance.PlayMusic(MusicManager.instance.defaultMusic);
        }
    }
}