using System;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public GameObject player, target1, target2;
    public float speed, attackTime;

    private readonly System.Random rnd = new System.Random();
    private GameObject nextTarget;
    private Animator animator;
    private Rigidbody2D rb;
    private bool inCombat = false;
    private float remainingOutCombat = 0, remainingAttack = 0;
    private int direction = 1;

    private void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        nextTarget = target2;
    }

    private void Idle () {
        animator.SetBool("Run", true);
        animator.SetBool("Jump", false);

        if(Vector2.Distance(transform.position, nextTarget.transform.position) < 2) {
            if(nextTarget == target1) nextTarget = target2;
            else nextTarget = target1;
            direction *= -1;
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);
        } else {
            transform.position = Vector2.MoveTowards(transform.position, nextTarget.transform.position, speed * Time.deltaTime);
        }
    }

    private void Combat () {
        if(player.transform.position.x > transform.position.x)
            direction = 1;
        else if(player.transform.position.x < transform.position.x)
            direction = -1;

        transform.localScale = new Vector3(Math.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);

        if(Vector2.Distance(player.transform.position, transform.position) < 2) {
            animator.SetBool("Run", false);
            animator.SetBool("Jump", false);
            animator.SetBool("inCombat", true);

            if(remainingAttack == 0) {
                int chance = rnd.Next(0, 100);
                if(chance <= 75) {
                    animator.SetTrigger("Attack");
                } else {
                    animator.SetBool("Jump", false);
                    animator.SetBool("Run", false);
                }
                remainingAttack = 1;
            } else {
                remainingAttack -= Time.deltaTime;
                if(remainingAttack < 0) remainingAttack = 0;
            }
        } else {
            animator.SetBool("Run", true);
            animator.SetBool("Jump", false);
            animator.SetBool("inCombat", true);

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        }
    }

    private void FixedUpdate () {
        if(Vector2.Distance(transform.position, player.transform.position) < 15 &&
                            ((direction == 1 && player.transform.position.x - transform.position.x >= 0) ||
                            (direction == -1 && player.transform.position.x - transform.position.x <= 0))) {
            inCombat = true;
            remainingOutCombat = 10;
        } else {
            if(remainingOutCombat > 0)
                remainingOutCombat -= Time.deltaTime;
            else {
                remainingOutCombat = 0;
                inCombat = false;
            }
        }

        if(!GetComponent<Enemy>().isDeath && !inCombat) {
            Idle();
            rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        } else if(!GetComponent<Enemy>().isDeath && inCombat)
            Combat();
    }
}
