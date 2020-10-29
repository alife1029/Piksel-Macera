using UnityEngine;

public class PlayerLightAttack : MonoBehaviour
{
    public int damage;
    System.Random rnd = new System.Random();

    private void Start () {
        damage = (SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData).attackPower;
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().GetDamage(GetComponentInParent<Rigidbody2D>().gameObject ,damage);
        }
    }
}
