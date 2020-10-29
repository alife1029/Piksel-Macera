using UnityEngine;

public class LevelUpTutorial : MonoBehaviour {
    public MessageText messageText;

    private string text = "BÖLÜMÜ BİTİRMEK İÇİN EVE GİRİN";

    private void OnTriggerEnter2D (Collider2D collision) {
        messageText.Alert(text, false);
        Invoke("DestroyThis", 3);
    }

    private void DestroyThis () {
        messageText.ResetMessage(text);
        Destroy(gameObject);
    }
}
