using UnityEngine;
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    // Rigidbody2D component reference (for physics movement)
    private Rigidbody2D rb;

    // Dash settings
    public float dashSpeed = 15f;       // How fast the player moves during dash
    public float dashDuration = 0.2f;   // How long the dash lasts
    public float dashCooldown = 1f;     // Time before next dash is allowed

    private float dashTimeLeft;         // Remaining dash time
    private float lastDashTime;         // When the last dash ended
    private bool isDashing = false;     // Whether the player is currently dashing

    // Movement input (from keyboard)
    private Vector2 moveInput;

    //Invincibility system
    public bool isInvincible = false;    // True when player can’t take damage
    public float invincibleDuration = 0.2f; // How long invincibility lasts (same as dash)

    // Optional visual feedback (like flash or trail)
    public SpriteRenderer spriteRenderer;
    public Color dashColor = Color.cyan;  // Temporary color during dash
    private Color originalColor;

    void Start()
    {
        // Get the Rigidbody2D attached to the same GameObject
        rb = GetComponent<Rigidbody2D>();

        // If you added a SpriteRenderer (optional)
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    void Update()
    {
        // Ignore movement input if currently dashing
        if (isDashing) return;

        // Get movement input (-1 to 1 range)
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize(); // Prevent faster diagonal movement

        // Check if player pressed Left Shift and dash is ready
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDashTime + dashCooldown)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        // --- Start dash ---
        isDashing = true;
        isInvincible = true;             // Become invincible
        dashTimeLeft = dashDuration;
        lastDashTime = Time.time;

        // Optional color feedback
        if (spriteRenderer != null)
            spriteRenderer.color = dashColor;

        // Store direction (so dash doesn’t change mid-movement)
        Vector2 dashDirection = moveInput;
        if (dashDirection == Vector2.zero)
            dashDirection = Vector2.right; // Default if not moving

        // Dash loop
        while (dashTimeLeft > 0)
        {
            rb.velocity = dashDirection * dashSpeed; // Move fast
            dashTimeLeft -= Time.deltaTime;
            yield return null; // Wait for next frame
        }

        // --- End dash ---
        rb.velocity = Vector2.zero;
        isDashing = false;

        // Optional color reset
        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;

        // Keep invincible a bit longer if needed (same as dashDuration here)
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;            //Lose invincibility
    }
}