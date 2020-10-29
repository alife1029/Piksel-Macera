using UnityEngine;

public class KillEnemy : MonoBehaviour {
    public MessageText messageText;
    string text = "DÜŞMANLARIN ÜSTESİNDEN GELMEK SİZE PUAN KAZANDIRIR";

    private void OnCollisionEnter2D (Collision2D collision) {
        messageText.Alert(text, false);
    }

    private void OnDestroy () {
        try {
            messageText.ResetMessage(text);
        } catch {}
    }
}
