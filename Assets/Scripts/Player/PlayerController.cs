using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int direction = 1;

    Animator anim;
    Rigidbody2D rb;
    PlayerStatus status;

    bool canJump = true, canAttack = true, ultiLoaded = true;
    int horizontalMove = 0, verticalMove = 0, hAttackMove = 0, lAttackMove = 0, uAttackMove = 0;
    float speed = 8, jumpSpeed = 450;

    private bool valuesInit = false;

    private void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        status = GetComponent<PlayerStatus>();
    }

    private void Update () {
        if(!valuesInit) {
            speed = (float)status.data.maxSpeed;
            valuesInit = true;
        }
        if(GetComponent<PlayerHealth>().health > 0) {
            // PC inputs
            if(!Application.isMobilePlatform) {
                // Attack inputs
                if(Input.GetAxis("Fire1") >= .2f) lAttackMove = 1;
                else lAttackMove = 0;
                if(Input.GetAxis("Fire2") >= .2f) hAttackMove = 1;
                else hAttackMove = 0;
                if(Input.GetAxis("Ultimate") >= .2f || (Input.GetAxis("Ultimate1") >= .2f && Input.GetAxis("Ultimate2") >= .2f)) uAttackMove = 1;
                else uAttackMove = 0;

                // Horizontal move input
                if(Input.GetAxis("Horizontal") >= .2f) horizontalMove = 1;
                else if(Input.GetAxis("Horizontal") <= -.2f) horizontalMove = -1;
                else horizontalMove = 0;

                // Vertical move input
                if(Input.GetAxis("Jump") >= .5f) verticalMove = 1;
                else verticalMove = 0;
            }

            direction = horizontalMove;
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

            // Light attack
            if(lAttackMove == 1 && canAttack) {
                if(SceneManager.GetActiveScene().buildIndex != 1)
                    status.data.swordUseCount += 1;
                canAttack = false;
                Invoke("ResetAttack", 0.5f);
                anim.SetTrigger("lightAttack");
            }

            // Heavy attack
            else if(hAttackMove == 1 && canAttack) {
                canAttack = false;
                Invoke("ResetAttack", 1);
                anim.SetTrigger("heavyAttack");
            }

            // Ultimate attack
            else if(uAttackMove == 1 && ultiLoaded) {
                if(SceneManager.GetActiveScene().buildIndex != 1)
                    status.data.swordUseCount += 1;
                ultiLoaded = false;
                Invoke("LoadUlti", 7);

                anim.SetTrigger("ultiAttack");
            }

            // Speed Limit
            if(rb.velocity.y < -30) {
                rb.velocity = new Vector2(speed * forceDirection, -30);
            } else if(rb.velocity.y > 30) {
                rb.velocity = new Vector2(speed * forceDirection, 30);
            } else {
                rb.velocity = new Vector2(speed * forceDirection, rb.velocity.y);
            }
        }
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if(collision.gameObject.tag == "Ground")
            canJump = true;
    }

    private void OnCollisionExit2D (Collision2D collision) {
        if(collision.transform.tag == "Ground")
            canJump = false;
    }

    private void ResetAttack () {
        canAttack = true;
    }

    private void LoadUlti () {
        ultiLoaded = true;
    }






    // Mobile button events
    public void onLeftPress () {
        horizontalMove = -1;
    }
    public void onLeftUp () {
        if(horizontalMove < 0) horizontalMove = 0;
    }

    public void onRightPress () {
        horizontalMove = 1;
    }
    public void onRightUp () {
        if(horizontalMove > 0) horizontalMove = 0;
    }

    public void onJumpPress () {
        verticalMove = 1;
    }
    public void onJumpUp () {
        verticalMove = 0;
    }

    public void onHeavyAttackPress () {
        hAttackMove = 1;
    }
    public void onHeavyAttackUp () {
        hAttackMove = 0;
    }

    public void onLightAttackPress () {
        lAttackMove = 1;
    }
    public void onLightAttackUp () {
        lAttackMove = 0;
    }
}
