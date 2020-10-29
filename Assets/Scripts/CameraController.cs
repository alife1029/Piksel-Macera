using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float damping;
    bool started = false;

    private void Start () { 
        transform.position = target.transform.position + offset;
        started = true;
    }

    private void FixedUpdate ()
    {
        if (started)
            transform.position = Vector3.Lerp(transform.position, target.position, damping) + offset;
    }
}
