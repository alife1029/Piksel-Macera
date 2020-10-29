using UnityEngine;

public class MessageText : MonoBehaviour
{
    public GameObject player;
    public AudioClip messageAudio;

    AudioSource audioSource;
    TextMesh textArea;
    private int direction = 1;

    private void Start () {
        textArea = GetComponent<TextMesh>();
        audioSource = GetComponentInParent<AudioSource>();
    }

    private void FixedUpdate () {
        if(player.GetComponent<PlayerController>().direction > 0 && direction == -1) {
            direction = 1;
            transform.localScale = new Vector2(direction, transform.localScale.y);
        } else if(player.GetComponent<PlayerController>().direction < 0 && direction == 1) {
            direction = -1;
            transform.localScale = new Vector2(direction, transform.localScale.y);
        }
    }

    public void Alert (string message, bool autoClose) {
        CancelInvoke();
        textArea.text = message;
        audioSource.PlayOneShot(messageAudio);

        if(autoClose)
            Invoke("ResetMessage", 2);
    }

    public void Alert (string message, float delay) {
        CancelInvoke();
        textArea.text = message;
        audioSource.PlayOneShot(messageAudio);
        Invoke("ResetMessage", delay);
    }

    private void ResetMessage () {
        textArea.text = null;
    }

    public void ResetMessage (string message) {
        if(textArea.text.Equals(message))
            ResetMessage();
    }
}
