using UnityEngine;
using UnityEngine.UI;

public class JumpTutorial : MonoBehaviour
{
    public Button jumpButton;
    public MessageText messageText;

    private Animator jumpButtonAnimator;
    private bool isStarted = false;
    private string textPC = "ZIPLAMAK İÇİN BOŞLUK TUŞUNA BASINIZ";
    private string textMobile = "ZIPLAMAK İÇİN BELİRTİLEN BUTONA BASINIZ";

    private void Start () {
        jumpButtonAnimator = jumpButton.GetComponent<Animator>();
        //if (Application.isMobilePlatform)
            jumpButton.GetComponent<Button>().onClick.AddListener(JumpButtonClick);
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            /*if(Application.isMobilePlatform)*/ jumpButtonAnimator.SetBool("winking", true);
            messageText.Alert(Application.isMobilePlatform ? textMobile : textPC, false);
            isStarted = true;
        }
    }

    private void JumpButtonClick() {
        jumpButtonAnimator.SetBool("winking", false);
        if (isStarted) Invoke("DeleteMessage", 1);
    }

    private void FixedUpdate ()
    {
        if (!Application.isMobilePlatform)
            if (isStarted && Input.GetAxis("Jump") > 0.5f)
                Invoke("DeleteMessage", 1);
    }

    private void DeleteMessage ()
    {
        messageText.ResetMessage(Application.isMobilePlatform ? textMobile : textPC);
        isStarted = false;
        gameObject.SetActive(false);
    }
}
