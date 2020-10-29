using System;

using UnityEngine;
using UnityEngine.UI;

using Achievement;
using Achievement.UI;

public class Enemy : MonoBehaviour
{
    public Slider xpBar;
    public Text level, xpBarText;
    public AudioClip[] audios;
    public HealthBar healthBar;

    public AchievementPopup achievementPopup;

    GameObject textArea;
    Animator anim;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    public int maxHealth, health, xpValue, direction;
    public bool isDeath = false;

    private bool canJump = true;

    private void Start ()
    {
        textArea = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MessageText>().gameObject;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        healthBar.maxValue = maxHealth;
        healthBar.value = health;

        xpValue *= (int) Math.Round((SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData).xpMultiplier);
    }

    private void FixedUpdate () {
        if(transform.localScale.x > 0)
            direction = 1;
        else direction = -1;
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            rb.velocity = new Vector2(0, rb.velocity.y);

            canJump = true;
        }

        if (collision.gameObject.tag == "Spike")
        {
            GetDamage(collision.gameObject ,health);
        }

        if (collision.gameObject.tag == "Death Border")
            GetDamage(collision.gameObject, health);
    }

    private void OnCollisionExit2D (Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
            canJump = false;
    }

    public void GetDamage (GameObject @object ,int amount)
    {
        if(!isDeath)
        {
            health -= amount;

            if (@object.gameObject.tag == "Player" && UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 1) {
                @object.GetComponent<PlayerStatus>().data.swordUseCount += 1;
            }

            if(health <= 0) {
                healthBar.gameObject.SetActive(false);
                textArea.GetComponent<MessageText>().Alert("+" + xpValue.ToString() + " DP", 1.3f);
                audioSource.PlayOneShot(audios[1]);
                anim.SetBool("isDeath", true);
                health = 0;
                isDeath = true;
                spriteRenderer.color = Color.red;

                if (xpBar.value + xpValue >= xpBar.maxValue) {
                    level.text = (int.Parse(level.text) + 1).ToString();
                    xpBar.value = (xpBar.value + xpValue) - xpBar.maxValue;
                    xpBar.maxValue += 10;
                } else {
                    xpBar.value += xpValue;
                }

                rb.velocity = new Vector2(0, rb.velocity.y);

                xpBarText.text = xpBar.value+"/"+xpBar.maxValue;

                if (@object.gameObject.tag == "Player") {
                    @object.GetComponent<PlayerStatus>().data.killCount += 1;
                    if(@object.GetComponent<PlayerStatus>().data.killCount == 1 && !AchievementSaveSystem.Read().bFirstBlood) {
                        AchievementController.AchievementCompleted(AchievementList.FIRST_BLOOD, achievementPopup);
                    }
                }

                Invoke("Delete", 7);
            } else {
                audioSource.PlayOneShot(audios[0]);
                healthBar.value = health;
                anim.SetTrigger("Hurt");

                // Hit effect
                if (@object.tag == "Player") {
                    float forceDirection = @object.GetComponent<PlayerController>().direction;
                    if(canJump && forceDirection > 0)
                        rb.AddForce(new Vector2(0.5f, 1) * 35000);
                    else if(canJump && forceDirection < 0)
                        rb.AddForce(new Vector2(-0.5f, 1) * 35000);
                    else if(!canJump && forceDirection > 0)
                        rb.AddForce(new Vector2(0.5f, -1) * 35000);
                    else if(!canJump && forceDirection < 0)
                        rb.AddForce(new Vector2(-0.5f, -1) * 35000);
                } else if (@object.tag == "Spike") {
                    rb.AddForce(400 * Vector2.up);
                }
            }
        }
    }

    public void GiveDamage (GameObject @object, int amount)
    {
        if(!isDeath)
        {
            if(@object.tag == "Player")
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().GetDamage(amount, false);
            }
        }
    }

    public int GetHealth () {
        return health;
    }

    private void Delete ()
    {
        Destroy(gameObject);
    }

    private void ResetColor ()
    {
        spriteRenderer.color = Color.white;
    }
}
