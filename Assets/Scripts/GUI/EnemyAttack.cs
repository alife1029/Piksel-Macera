using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool isActive;
    public int[] damage = new int[2];

    readonly System.Random rnd = new System.Random();

    private void OnTriggerEnter2D (Collider2D collision) {
        if(collision.gameObject.tag == "Player" && isActive) {
            collision.gameObject.GetComponent<PlayerHealth>().GetDamage(rnd.Next(3, 7), false);
        }
    }
}
