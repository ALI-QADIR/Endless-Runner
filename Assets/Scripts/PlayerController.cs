using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public variables with tooltips
    [Tooltip("Force with which the player jumps")] public float jumpForce = 700;        // jump force
    [Tooltip("Gravity multiplier")] public float gravityModifier = 1;                   // gravity modifier
    [Tooltip("Player is on ground")] public bool isOnGround = true;              // is on ground
    public bool gameOver = false;                                                       // game over

    // private variables
    private Rigidbody rb;                                                               // rigidbody component
    // private static bool gameOver = GameManager.gameOver;                                // game over
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();                                                 // get rigidbody component
        Physics.gravity *= gravityModifier;                                             // set gravity
    }

    // Update is called once per frame
    void Update()
    {
        VerticalMovement();                                                             // call vertical movement function
    }

    void VerticalMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)                              // if space is pressed
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);                     // add force to rigidbody
            isOnGround = false;                                                         // set is on ground to false
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))                                 // if collision with ground
        {
            isOnGround = true;                                                          // set is on ground to true
        }
        else if (collision.gameObject.CompareTag("Obstacle"))                          // if collision with obstacle
        {
            gameOver = true;                                                            // set game over to true
            Debug.Log("Game Over");                                                     // log game over
        }
    }
}
