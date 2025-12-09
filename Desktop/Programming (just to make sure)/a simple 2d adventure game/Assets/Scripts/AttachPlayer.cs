using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If player stands on the platform
        if (collision.gameObject.CompareTag("player"))
        {
            // Make the player a child of the platform so they move together
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // When player leaves the platform
        if (collision.gameObject.CompareTag("player"))
        {
            // Remove the parent relationship
            collision.transform.SetParent(null);
        }
    }
}