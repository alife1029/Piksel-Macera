using UnityEngine;

public class ShortcutTutorial : MonoBehaviour
{
    public MessageText messageText;
    
    private string text = "EĞER ZORLU PARKURLARDA İYİYSEN DÜZ GİT \n" + 
                            "EĞER SAVAŞMADA İYİYSEN AŞAĞIYA ATLA";

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            messageText.Alert(text, false);
            Invoke ("DeleteMessage", 5);
        }
    }

    private void DeleteMessage () {
        messageText.ResetMessage(text);
        Destroy(gameObject);
    }
}
