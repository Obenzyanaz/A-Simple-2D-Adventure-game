using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float speed = 2f;

    public Transform bottomPoint;
    public Transform topPoint;

    private Transform currentTarget;
    private bool playerOnElevator = false;

    public bool useWaitTime = true;
    public float minWait = 5f;
    public float maxWait = 10f;

    private bool isWaiting = false;
    private float waitTimer = 0f;

    void Start()
    {
        // Force the elevator to spawn EXACTLY at the bottom point
        transform.position = new Vector2(
            transform.position.x,
            bottomPoint.position.y
        );

        currentTarget = bottomPoint;
    }

    void Update()
    {
        // Only move upward if player is standing on it
        if (!playerOnElevator && currentTarget == topPoint)
            return;

        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0f || !playerOnElevator)
            {
                isWaiting = false;
                currentTarget = bottomPoint;
            }
            return;
        }

        // Move ONLY vertically (no sideways drift)
        float newY = Mathf.MoveTowards(
            transform.position.y,
            currentTarget.position.y,
            speed * Time.deltaTime
        );

        transform.position = new Vector2(transform.position.x, newY);

        // Check if reached TOP
        if (currentTarget == topPoint &&
            Mathf.Abs(transform.position.y - topPoint.position.y) <= 0.01f)
        {
            if (useWaitTime)
            {
                isWaiting = true;
                waitTimer = Random.Range(minWait, maxWait);
            }
            else
            {
                if (!playerOnElevator)
                    currentTarget = bottomPoint;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("player"))
        {
            playerOnElevator = true;
            currentTarget = topPoint; // start going up
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("player"))
        {
            playerOnElevator = false;

            if (!useWaitTime)
                currentTarget = bottomPoint;
        }
    }
}