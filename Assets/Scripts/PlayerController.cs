using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public variables with tooltips
    [Tooltip("Force with which the player jumps")] public float jumpForce = 700;                  // jump force
    [Tooltip("Gravity multiplier")] public float gravityModifier = 1;                             // gravity modifier
    [Tooltip("Player is on ground")] public bool isOnGround = true;                               // is on ground
    public bool gameOver = false;                                                                 // game over
    [Tooltip("Explosion Particle System for Death")] public ParticleSystem explosionParticle;     // explosion particle system
    [Tooltip("Dirt Particle System for trail")] public ParticleSystem dirtParticle;               // dirt particle system

    // private variables
    private Animator playerAnim;                                                                  // animator on player
    private Rigidbody rb;                                                                         // rigidbody component
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();                                                           // get rigidbody component
        Physics.gravity *= gravityModifier;                                                       // set gravity
        playerAnim = GetComponent<Animator>();                                                    // get animator component on player
    }

    // Update is called once per frame
    void Update()
    {
        VerticalMovement();                                                                       // call vertical movement function
    }

    void VerticalMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)                           // if space is pressed
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);                               // add force to rigidbody
            isOnGround = false;                                                                   // set is on ground to false
            playerAnim.SetTrigger("Jump_trig");                                                   // set jump trigger
            dirtParticle.Stop();                                                                  // stop dirt particle system
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))                                            // if collision with ground
        {
            isOnGround = true;                                                                    // set is on ground to true
            dirtParticle.Play();                                                                  // play dirt particle system
        }
        else if (collision.gameObject.CompareTag("Obstacle"))                                     // if collision with obstacle
        {
            gameOver = true;                                                                      // set game over to true
            Debug.Log("Game Over");                                                               // log game over
            playerAnim.SetBool("Death_b", true);                                                  // set death animation
            playerAnim.SetInteger("DeathType_int", 1);                                            // set death type
            explosionParticle.Play();                                                             // play the particle system

            dirtParticle.Stop();                                                                  // stop dirt particle system
        }
    }
}
