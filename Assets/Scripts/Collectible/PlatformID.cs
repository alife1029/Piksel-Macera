using System;
using UnityEngine;
using UnityEngine.UI;

public class PlatformID : MonoBehaviour
{
    public GameObject messageArea;
    public Slider xpBar;
    public Text xpBarText, xpLevelText;

    public int xpValue = 0;

    private bool started = false;
    private bool finished = false;

    private void Start () {
        xpValue *= (int) Math.Round((SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData).xpMultiplier);
    }

    public void SetStarted (bool started) {
        this.started = started;
    }
    public bool IsStarted () {
        return started;
    }

    public void SetFinished (bool finished) {
        if(started) {
            this.finished = finished;
            messageArea.GetComponent<MessageText>().Alert("+" + xpValue.ToString() + " DP", 0.7f);

            float dif = xpBar.maxValue - xpBar.value;
            if(dif > xpValue) {
                xpBar.value += xpValue;
            } else {
                xpBar.maxValue += 10;
                xpBar.value = dif;
                xpLevelText.text = (int.Parse(xpLevelText.text) + 1).ToString();
            }
            xpBarText.text = xpBar.value.ToString() + "/" + xpBar.maxValue.ToString();
        }
    }
    public bool IsFinished () {
        return finished;
    }
}
