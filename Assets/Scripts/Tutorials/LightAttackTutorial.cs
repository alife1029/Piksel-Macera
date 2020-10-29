using UnityEngine;
using UnityEngine.UI;

public class LightAttackTutorial : MonoBehaviour
{
    public Button lightAttackButton;
    public MessageText messageText;
    private Animator lightAttackButtonAnimator;
    private bool isStartted = false;
    string textPC = "BİR HAYDUT! HAFİF SALDIRI YAPMAK İÇİN SOL TIKLAYINIZ";
    string textMobile = "BİR HAYDUT! HAFİF SALDIRI YAPMAK BELİRTİLEN BUTONA BASINIZ";

    private void Start () {
        if (Application.isMobilePlatform) {
            lightAttackButtonAnimator = lightAttackButton.GetComponent<Animator>();
            lightAttackButton.onClick.AddListener(LightAttackButton_Click);
        }
    }

    private void LightAttackButton_Click () {
        if (isStartted) {
            messageText.ResetMessage(textMobile);
            lightAttackButtonAnimator.SetBool("winking", false);
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate ()
    {
        if (!Application.isMobilePlatform) {
            if(isStartted && Input.GetAxis("Fire1") > 0.5) {
                messageText.ResetMessage(textPC);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            messageText.Alert(Application.isMobilePlatform ? textMobile : textPC, false);
            isStartted = true;

            if (Application.isMobilePlatform) {
                lightAttackButtonAnimator.SetBool("winking", true);
            }
        }
    }
}
