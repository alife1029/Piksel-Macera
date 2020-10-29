using UnityEngine;

public class Food : MonoBehaviour {
    public int healAmount;

    private void OnTriggerEnter2D (Collider2D collision) {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if(collision.gameObject.tag == "Player" && playerHealth.health < playerHealth.maxHealth) {
            collision.GetComponent<PlayerHealth>().Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
