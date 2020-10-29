using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject buttons;

    void Start()
    {
        // If game is running in a mobile device show the buttons, else hide the buttons
        if (Application.isMobilePlatform) {
            buttons.SetActive(true);
        } else {
            buttons.SetActive(false);
        }
    }
}
