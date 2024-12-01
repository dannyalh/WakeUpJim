using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource footstepAudio;
    private Vector2 moveInput;

    public float movementThreshold = 0.1f;

    private string currentWorld; // Track whether Jim is in Real or Dream World

    void Start()
    {
        InitializeComponents();

        // Determine the current world based on scene name or tags
        currentWorld = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Debug.Log("Current World: " + currentWorld);
    }

    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        footstepAudio = GetComponent<AudioSource>();

        if (footstepAudio == null)
        {
            Debug.LogError("No AudioSource found for footstep sounds!");
        }
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);
        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        HandleFootstepSounds();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    private void HandleFootstepSounds()
    {
        if (moveInput.sqrMagnitude > movementThreshold)
        {
            if (!footstepAudio.isPlaying)
            {
                footstepAudio.Play();
            }
        }
        else
        {
            if (footstepAudio.isPlaying)
            {
                footstepAudio.Stop();
            }
        }
    }
}
