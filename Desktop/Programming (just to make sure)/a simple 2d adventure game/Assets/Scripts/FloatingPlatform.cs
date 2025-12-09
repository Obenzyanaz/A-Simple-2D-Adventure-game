using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Speed of movement
    public float speed = 2f;

    // Target position the platform is moving toward
    private Transform currentTarget;

    // References to both points between which the platform moves
    public Transform pointA;
    public Transform pointB;

    // This toggle lets you choose if the platform moves horizontally, vertically, or both
    public bool moveHorizontally = true;
    public bool moveVertically = false;

    void Start()
    {
        // Start moving toward PointB first
        currentTarget = pointB;
    }

    void Update()
    {
        // Determine target position based on what kind of movement we allow
        Vector2 targetPosition = currentTarget.position;

        // If we only want horizontal movement, keep the platform's current Y position fixed
        if (moveHorizontally && !moveVertically)
        {
            targetPosition = new Vector2(targetPosition.x, transform.position.y);
        }
        // If we only want vertical movement, keep the platform's current X position fixed
        else if (moveVertically && !moveHorizontally)
        {
            targetPosition = new Vector2(transform.position.x, targetPosition.y);
        }
        // If both are true, it will move diagonally toward the full target position

        // Move the platform toward the adjusted target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    // Trigger when the platform reaches one of the points
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the platform hits something with the "PlatformPoint" tag
        if (collision.CompareTag("PlatformPoint"))
        {
            // Change direction depending on which point we reached
            if (currentTarget == pointB)
            {
                currentTarget = pointA; // go back
            }
            else
            {
                currentTarget = pointB; // go forward
            }
        }
    }
}