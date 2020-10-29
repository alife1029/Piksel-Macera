using UnityEngine;

public class Player : MonoBehaviour
{/*
    Animator anim;
    Rigidbody2D rb;

    bool canJump = true;
    int horizontalMove = 0, verticalMove = 0;
    float speed = 8, jumpSpeed = 450;
    private void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update () {
        // PC inputs
        if(!Application.isMobilePlatform) {
            // Horizontal move input
            if(Input.GetAxis("Horizontal") >= .2f) horizontalMove = 1;
            else if(Input.GetAxis("Horizontal") <= -.2f) horizontalMove = -1;
            else horizontalMove = 0;

            // Vertical move input
            if(Input.GetAxis("Jump") >= .5f) verticalMove = 1;
            else verticalMove = 0;
        }

        float forceDirection = horizontalMove;

        // Running
        if(forceDirection != 0) {
            anim.SetBool("isRunning", true);

            if(forceDirection < 0)
                transform.localScale = new Vector2(-13, transform.localScale.y);

            else
                transform.localScale = new Vector2(13, transform.localScale.y);
        } else {
            anim.SetBool("isRunning", false);
        }

        // Jumping
        if(verticalMove != 0 && canJump) {
            canJump = false;
            rb.AddForce(Vector2.up * jumpSpeed);
        }

        // Speed Limit
        if(rb.velocity.y < -12) {
            rb.velocity = new Vector2(speed * forceDirection, -12);
        } else if(rb.velocity.y > 12) {
            rb.velocity = new Vector2(speed * forceDirection, 12);
        } else {
            rb.velocity = new Vector2(speed * forceDirection, rb.velocity.y);
        }
    }*/
}
