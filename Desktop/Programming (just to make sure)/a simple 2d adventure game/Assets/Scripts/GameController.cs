using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Checkpoint & Lives")]
    Vector2 checkpointpos;                   // Where the player respawns when they die
    [SerializeField] int maxLives = 3;       // Maximum number of lives
    int currentLives;                        // Current number of lives left

    [Header("Health Settings")]
    [SerializeField] int maxHealth = 100;    // Maximum health (HP)
    int currentHealth;                       // Current HP value
    [SerializeField] float invincibilityDuration = 1f; // Duration of invulnerability after getting hit
    bool isInvincible = false;               // Whether the player is currently invincible

    [Header("Respawn Points")]
    [SerializeField] Transform spikeRespawnPoint; // Where player goes after hitting spikes

    [Header("Components")]
    Rigidbody2D playerRb;                    // Rigidbody component for physics
    Animator animator;                       // Animator for animations
    SpriteRenderer spriteRenderer;           // For player sprite (blinking, visibility)
    PlayerMovement playerMovement;           // Reference to movement script
    [SerializeField] FadeController fadeController; // Handles fade-in/out effects

    [Header("UI")]
    [SerializeField] GameObject gameOverUI;  // Game Over screen
    [SerializeField] LifeDisplay lifeDisplay;// UI for showing lives

    private void Awake()
    {
        // Grab references to required components attached to player
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        checkpointpos = transform.position;                       // Set first checkpoint
        currentLives = DifficultyManager.instance.startingLives;  // Lives from difficulty settings
        currentHealth = maxHealth;                                // Start with full health
        gameOverUI.SetActive(false);                              // Hide Game Over screen
        UpdateLivesUI();                                          // Show initial lives on screen
    }

    // Detect collisions with enemies or spikes
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Spikes
        if (collision.CompareTag("Obstacle"))
        {
            TakeDamage(50, true);  // Lose 50 HP and teleport to spike respawn
        }

        // Enemy
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(20, false); // Lose 20 HP, stay in place, get i-frames
        }
    }

    // Function to update checkpoint from Checkpoint script
    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointpos = pos;
    }

    // ========================================
    // DAMAGE + HEALTH SYSTEM
    // ========================================
    void TakeDamage(int damage, bool isSpike)
    {
        // Ignore damage during invincibility
        if (isInvincible) return;

        currentHealth -= damage; // Reduce HP

        if (animator != null)
            animator.SetTrigger("Hurt"); // Play hurt animation

        if (isSpike)
        {
            // Teleport player to a spike-specific respawn point
            StartCoroutine(SpikeRespawn());
        }
        else
        {
            // Temporary invulnerability after enemy hit
            StartCoroutine(InvincibilityFrames());
        }

        // Check if player health reaches 0
        if (currentHealth <= 0)
        {
            Die(); // Handle death (lose a life or game over)
        }
    }

    // ========================================
    // INVINCIBILITY & BLINKING EFFECT
    // ========================================
    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        float elapsed = 0f;

        // Store the original color
        Color originalColor = spriteRenderer.color;

        while (elapsed < invincibilityDuration)
        {
            // Turn white
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);

            // Back to normal
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.1f);

            elapsed += 0.2f; // total per cycle
        }

        // Make sure the player is visible and back to normal color
        spriteRenderer.color = originalColor;
        isInvincible = false;
    }

    // ========================================
    // SPIKE RESPAWN (teleport to another point)
    // ========================================
    IEnumerator SpikeRespawn()
    {
        playerRb.simulated = false;
        playerMovement.enabled = false;

        yield return StartCoroutine(fadeController.FadeOut(0.5f)); // Fade to black

        // Move to spike respawn point
        if (spikeRespawnPoint != null)
            transform.position = spikeRespawnPoint.position;
        else
            transform.position = checkpointpos; // fallback if not set

        // Optional: heal a bit when respawning from spikes
       // currentHealth = Mathf.Clamp(currentHealth + 20, 0, maxHealth);

        yield return StartCoroutine(fadeController.FadeIn(0.5f)); // Fade in

        playerMovement.enabled = true;
        playerRb.simulated = true;
    }

    // ========================================
    // PLAYER DEATH + RESPAWN
    // ========================================
    void Die()
    {
        if (animator != null)
            animator.SetTrigger("Die");

        currentLives--;
        UpdateLivesUI();

        if (currentLives <= 0)
        {
            StartCoroutine(DieAndGameOver());
        }
        else
        {
            currentHealth = maxHealth; // Reset HP after respawn
            StartCoroutine(DieAndRespawn(0.5f));
        }

        // Reset music after death
        MusicManager.instance.PlayMusic(MusicManager.instance.defaultMusic);
    }

    // Respawn player after death
    IEnumerator DieAndRespawn(float duration)
    {
        playerRb.simulated = false;
        playerRb.velocity = Vector2.zero;
        playerMovement.enabled = false;
        spriteRenderer.enabled = false;

        yield return StartCoroutine(fadeController.FadeOut(0.5f));
        yield return new WaitForSeconds(duration);

        transform.position = checkpointpos;     // Respawn at checkpoint
        transform.localScale = new Vector3(10, 10, 1);
        currentHealth = maxHealth;

        spriteRenderer.enabled = true;
        yield return StartCoroutine(fadeController.FadeIn(0.5f));

        playerMovement.enabled = true;
        playerRb.simulated = true;

        if (animator != null)
            animator.SetTrigger("Respawn");
    }

    // ========================================
    // GAME OVER LOGIC
    // ========================================
    IEnumerator DieAndGameOver()
    {
        playerRb.simulated = false;
        playerRb.velocity = Vector2.zero;
        playerMovement.enabled = false;
        spriteRenderer.enabled = true;

        yield return StartCoroutine(fadeController.FadeOut(0.5f));
        yield return new WaitForSeconds(1f);

        if (animator != null)
            animator.SetTrigger("GameOver");

        MusicManager.instance.PlayMusic(MusicManager.instance.gameOverMusic);
        gameOverUI.SetActive(true);
    }

    // ========================================
    // UI UPDATER
    // ========================================
    void UpdateLivesUI()
    {
        if (lifeDisplay != null)
            lifeDisplay.UpdateDisplay(currentLives);
    }
}
