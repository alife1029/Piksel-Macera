using UnityEngine;

public class MessageWriter : MonoBehaviour {
    public GameObject[] otherObjects;
    public TextMesh messageArea;
    
    [Multiline]

    public string text;
    public float time;
    public bool destroyGameObject;



    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            messageArea.text = text;
            Invoke("DeleteMessage", time);
        }

        foreach(GameObject @object in otherObjects)
            Destroy(@object);
    }

    private void DeleteMessage () {
        if (messageArea.text == text)
            messageArea.text = null;
        if (destroyGameObject)
            Destroy(this.gameObject);
    }
}
