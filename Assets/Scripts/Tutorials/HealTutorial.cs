using UnityEngine;

public class HealTutorial : MonoBehaviour
{
    public MessageText messageText;
    string text = "EĞER CANIN AZALDIYSA YİYECEKLERLE CANINI ARTTIRABİLİRSİN";

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            messageText.Alert(text, false);
            Invoke("ResetText", 6.0f);
        }
    }

    private void ResetText () {
        messageText.ResetMessage(text);
        gameObject.SetActive(false);
    }
}
