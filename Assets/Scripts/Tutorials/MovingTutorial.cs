using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovingTutorial : EventTrigger
{
    public Button[] moveButtons;
    public MessageText messageText;
    private bool isStarted = false;

    private Animator[] buttonAnimators;
    private string textPC = "HAREKET ETMEK İÇİN 'A, D' YA DA OK TUŞLARINI KULLANIN";
    private string textMobile = "HAREKET ETMEK İÇİN BELİRTİLEN TUŞLARI KULLANINIZ";

    private void Start () {
        buttonAnimators = new Animator[moveButtons.Length];
        for(int i = 0; i < moveButtons.Length; i++) {
            buttonAnimators[i] = moveButtons[i].GetComponent<Animator>();
        }

        if(Application.isMobilePlatform)
            foreach(Button button in moveButtons)
                button.onClick.AddListener(buttonClick);

        Invoke("Init", 0.3f);
    }

    private void buttonClick() {
        if(isStarted) Invoke("Close", 1f);
    }

    private void FixedUpdate () {
        if(!Application.isMobilePlatform)
            if(Input.GetAxis("Horizontal") != 0 && isStarted)
                Invoke("Close", 1f);
    }

    private void Init () {
        isStarted = true;
        messageText.Alert(Application.isMobilePlatform ? textMobile : textPC, false);
        if (Application.isMobilePlatform) 
            foreach(Animator animator in buttonAnimators)
                animator.SetBool("winking", true);
    }

    private void Close () {
        if(Application.isMobilePlatform)
            foreach(Animator animator in buttonAnimators)
                animator.SetBool("winking", false);

        messageText.ResetMessage(Application.isMobilePlatform ? textMobile : textPC);

        isStarted = false;
        enabled = false;
    }
}
