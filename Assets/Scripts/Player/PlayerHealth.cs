using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    public Text healthBarText;
    public AudioClip[] audios;
    public GameObject messageArea;

    public int maxHealth;
    public int health;

    AudioSource audioSource;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    private bool valuesInited;
    private float defencePower;

    private void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        HealthBar();
    }

    private void Update () {
        if(!valuesInited) {
            PlayerData data = SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData;
            maxHealth = data.maxHealth;
            health = SceneManager.GetActiveScene().buildIndex == 1 ? 12 : maxHealth;
            defencePower = (float) data.defencePower;
            HealthBar();
            valuesInited = true;
        }
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        // Spike Damage
        if (collision.gameObject.tag == "Spike")
        {
            rb.AddForce(Vector2.up * 500);
            GetDamage(maxHealth, true);
        }
    }

    public void GetDamage (float amount, bool maximum)
    {
        if(!maximum) {
            amount = (int)(amount /= defencePower);
            messageArea.GetComponent<MessageText>().Alert("-" + amount.ToString("F0") + " CAN", 1.3f);
        }

        if (health > 0) {
            health -= (int) amount;
            spriteRenderer.color = Color.red;
            Invoke("ResetApperance", 0.15f);

            if(health <= 0) {
                PlayerData playerData = SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData;
                audioSource.PlayOneShot(audios[1]);
                CancelInvoke();
                health = 0;
                rb.Sleep();
                transform.Rotate(new Vector3(0, 0, 90));
                playerData.starting = "Load Checkpoint"; // Save the load from checkpoint
                SaveSystem.Save(playerData, SaveSystem.playerDataPath);
                Invoke("Restart", 0.7f);
            } else audioSource.PlayOneShot(audios[0]);
        } else {
            PlayerData playerData = SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData;
            audioSource.PlayOneShot(audios[1]);
            CancelInvoke();
            health = 0;
            rb.Sleep();
            transform.Rotate(new Vector3(0, 0, 90));
            playerData.starting = "Load Checkpoint"; // Save the load from checkpoint
            SaveSystem.Save(playerData, SaveSystem.playerDataPath);
            Invoke("Restart", 0.7f);
        }

        HealthBar();
    }

    public void Heal (int amount)
    {
        messageArea.GetComponent<MessageText>().Alert(
            "+" + (amount > maxHealth - health ? (maxHealth - health).ToString() : amount.ToString()) + " CAN", 1.3f
        );
        health += amount;
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(audios[2]);
        if(health > maxHealth)
            health = maxHealth;

        HealthBar();
    }

    private void HealthBar () {
        healthBar.maxValue = maxHealth;
        healthBar.value = health;

        healthBarText.text = health.ToString() + "/" + maxHealth.ToString();
    }

    private void ResetApperance ()
    {
        spriteRenderer.color = Color.white;
    }

    private void Restart ()
    {
        GetComponent<PlayerStatus>().data.deathCount += 1;
        GetComponent<PlayerStatus>().Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
