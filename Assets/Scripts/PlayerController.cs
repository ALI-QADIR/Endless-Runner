using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public variables with tooltips
    [Header("Jump variables")]
    [Tooltip("Force with which the player jumps")] public float jumpForce = 700;                  // jump force
    [Tooltip("Gravity multiplier")] public float gravityModifier = 1;                             // gravity modifier
    [Tooltip("Super Jump Force")] public float superJumpForce = 1000;                             // super jump force

    [Header("Particle Systems")]
    [Tooltip("Explosion Particle System for Death")] public ParticleSystem explosionParticle;     // explosion particle system
    [Tooltip("Dirt Particle System for trail")] public ParticleSystem dirtParticle;               // dirt particle system

    [Header("Audio components")]
    [Tooltip("Audio clip for jump")] public AudioClip jumpAudio;                                  // Audio clip for jump
    [Tooltip("Audio clip for crash")] public AudioClip crashAudio;                                // Audio clip for crash

    [HideInInspector]
    public bool gameOver = false;
    [HideInInspector]
    public bool isOnGround = true;                                                                // is on ground
    [HideInInspector]
    public bool isDashing;

    // private variables
    private Animator playerAnim;                                                                  // animator on player
    private Rigidbody rb;                                                                         // rigidbody component
    private AudioSource playerAudio;                                                              // Player Audio Source
    private bool doubleJumpUsed;                                                                  // double jump used
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();                                                           // get rigidbody component
        Physics.gravity *= gravityModifier;                                                       // set gravity
        playerAnim = GetComponent<Animator>();                                                    // get animator component on player
        playerAudio = GetComponent<AudioSource>();                                                // get audio source component on player
    }

    // Update is called once per frame
    void Update()
    {
        VerticalMovement();                                                                       // call vertical movement function
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDashing = true;
            playerAnim.SetFloat("Speed_Mul", 2.0f);
            // Debug.Log("Dash");
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isDashing = false;
            playerAnim.SetFloat("Speed_Mul", 1.0f);
            // Debug.Log("No Dash");
        }
    }

    void VerticalMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)                           // if space is pressed
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);                               // add force to rigidbody
            isOnGround = false;                                                                   // set is on ground to false
            playerAnim.SetTrigger("Jump_trig");                                                   // set jump trigger
            playerAudio.PlayOneShot(jumpAudio, 1.0f);                                             // play jump audio once
            dirtParticle.Stop();                                                                  // stop dirt particle system
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJumpUsed) 
        {
            rb.AddForce(Vector3.up * superJumpForce, ForceMode.Impulse);
            doubleJumpUsed = true;
            playerAnim.Play("Running_Jump", 3, 0f);
            playerAudio.PlayOneShot(jumpAudio, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))                                            // if collision with ground
        {
            isOnGround = true;                                                                    // set is on ground to true
            doubleJumpUsed = false;                                                               // set double jump used to false
            dirtParticle.Play();                                                                  // play dirt particle system
        }
        else if (collision.gameObject.CompareTag("Obstacle"))                                     // if collision with obstacle
        {
            gameOver = true;                                                                      // set game over to true
            Debug.Log("Game Over");                                                               // log game over
            playerAnim.SetBool("Death_b", true);                                                  // set death animation
            playerAnim.SetInteger("DeathType_int", 1);                                            // set death type
            explosionParticle.Play();                                                             // play the particle system
            playerAudio.PlayOneShot(crashAudio, 1.0f);                                            // play crash audio once
            dirtParticle.Stop();                                                                  // stop dirt particle system
            playerAudio.PlayOneShot(crashAudio, 1.0f);                                            // play crash audio once
        }
    }
}
