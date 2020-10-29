using UnityEngine;

public class DynamicPlatform : MonoBehaviour {
    public float speed;
    public Transform firstPos, secondPos;
    Vector3 nextPos;

    private void Start () {
        nextPos = firstPos.position;
    }

    private void FixedUpdate () {
        if(transform.position == firstPos.position)
            nextPos = secondPos.position;
        else if(transform.position == secondPos.position)
            nextPos = firstPos.position;

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    // Stick the objects to dynamic platform
    private void OnCollisionEnter2D (Collision2D collision) {
        collision.transform.SetParent(this.transform);
        //exCondition1 = false;
    }

    private void OnCollisionExit2D (Collision2D collision) {
        //exCondition1 = true;
        //if(exCondition1 && exCondition2)
        collision.transform.SetParent(null);
    }

    // Draw line
    private void OnDrawGizmos () {
        Gizmos.DrawLine(firstPos.position, secondPos.position);
    }
}
