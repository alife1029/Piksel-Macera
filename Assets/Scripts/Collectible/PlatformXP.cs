using UnityEngine;

public class PlatformXP : MonoBehaviour
{
    public string task;
    PlatformID parent;

    private void Start () {
        parent = GetComponentInParent<PlatformID>();
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if(collision.gameObject.tag.Equals("Player")) {
            if(task.Equals("Entry"))
                parent.SetStarted(true);
            else if(task.Equals("End")) {
                if(parent.IsStarted())
                    parent.SetFinished(true);
            } else {
                parent.SetStarted(false);
                parent.SetFinished(false);
            }

            Destroy(gameObject);
        }
    }
}
