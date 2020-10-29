using UnityEngine;

public class KillHeavyEnemy : MonoBehaviour
{
    public MessageText messageText;
    string text = "GÜÇLÜ DÜŞMANLARIN ÜSTESİNDEN GELMEK SİZE DAHA FAZLA PUAN KAZANDIRIR";

    private void FixedUpdate () {
        if (gameObject.GetComponent<Enemy>().GetHealth() <= 0) {
            messageText.ResetMessage(text);
            Destroy(this);
        }
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        messageText.Alert(text, false);
    }
}
