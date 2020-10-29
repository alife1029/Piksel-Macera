using UnityEngine;
using UnityEngine.UI;

public class HeavyAttackTutorial : MonoBehaviour
{
    public Button heavyAttackButton;
    public MessageText messageText;
    private Animator heavyAttackButtonAnimator;
    private bool isStartted = false;
    string textPC = "DAHA GÜÇLÜ BİR HAYDUT! AĞIR SALDIRI YAPMAK İÇİN SAĞ TIKLAYINIZ";
    string textMobile = "DAHA GÜÇLÜ BİR HAYDUT! AĞIR SALDIRI YAPMAK İÇİN BELİRTİLEN BUTONA BASIN";

    private void Start () {
        if (Application.isMobilePlatform) {
            heavyAttackButtonAnimator = heavyAttackButton.GetComponent<Animator>();
            heavyAttackButton.onClick.AddListener(heavyAttackButton_Click);
        }
    }

    private void heavyAttackButton_Click () {
        if(isStartted) {
            messageText.ResetMessage(Application.isMobilePlatform ? textMobile : textPC);
            if (Application.isMobilePlatform)
                heavyAttackButtonAnimator.SetBool("winking", false);
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate () {
        if (!Application.isMobilePlatform) {
            if(isStartted && Input.GetAxis("Fire2") > 0.5) {
                messageText.ResetMessage(Application.isMobilePlatform ? textMobile : textPC);
                gameObject.SetActive(false);
            }
        }
    }
    
    private void OnTriggerEnter2D (Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            messageText.Alert(Application.isMobilePlatform ? textMobile : textPC, false);
            isStartted = true;
            if (Application.isMobilePlatform)
                heavyAttackButtonAnimator.SetBool("winking", true);
        }
    }
}
