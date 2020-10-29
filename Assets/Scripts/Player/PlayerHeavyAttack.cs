using UnityEngine;

public class PlayerHeavyAttack : MonoBehaviour
{
    public int damage;
    System.Random rnd = new System.Random();

    private void Start () {
        damage = Mathf.RoundToInt((SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData).attackPower * 1.5f);
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().GetDamage(GetComponentInParent<Rigidbody2D>().gameObject, damage);
        }
    }
}
