using UnityEngine;

public class MovingPlatformTutorial : MonoBehaviour
{
    public MessageText messageText;
    string text = "HAREKETLİ PLATFORMLARIN ÜSTÜNE ÇIKARAK HAREKET EDEBİLİRSİNİZ";
    bool started = false;

    private void Update () {
        if(Input.GetAxis("Jump") > 0.5f && started)
            Invoke("DestroyThis", 1f);
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            messageText.Alert(text, false);
            started = true;
        }
    }

    private void DestroyThis () {
        Destroy(gameObject);
    }

    private void OnDestroy () {
        try {
            messageText.ResetMessage(text);
        } catch {}
    }
}
