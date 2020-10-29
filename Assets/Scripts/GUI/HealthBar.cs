using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject fillArea;
    public TextMesh barText;
    public float value, maxValue;
    private float direction = 1;

    private void Update () {
        // Setting value
        if(maxValue > value) {
            float dif = -(0.5f - (value / maxValue) / 2) * fillArea.transform.localScale.y;

            if(value > 0) {
                fillArea.transform.localPosition = new Vector2(dif, fillArea.transform.localPosition.y);


                fillArea.transform.localScale = new Vector3(value / maxValue, fillArea.transform.localScale.y, fillArea.transform.localScale.z);
                fillArea.transform.localScale = new Vector3(-(value / maxValue), fillArea.transform.localScale.y, fillArea.transform.localScale.z);
            } else {
                value = 0;
                fillArea.transform.localPosition = new Vector2(0, fillArea.transform.localPosition.y);
                fillArea.transform.localScale = new Vector3(0, fillArea.transform.localScale.y, fillArea.transform.localScale.z);
            }
        } else {
            value = maxValue;
            fillArea.transform.localPosition = new Vector2(0, fillArea.transform.localPosition.y);
            fillArea.transform.localScale = new Vector3(1, fillArea.transform.localScale.y, fillArea.transform.localScale.z);
        }

        // Setting direction
        if(GetComponentInParent<Enemy>().direction > 0 && direction < 0) {
            direction = -1 * transform.localScale.x;
            this.transform.localScale = new Vector3(direction, this.transform.localScale.y, transform.localScale.z);
        } else if(GetComponentInParent<Enemy>().direction < 0 && direction > 0) {
            direction = -1 * transform.localScale.x;
            this.transform.localScale = new Vector3(direction, this.transform.localScale.y, transform.localScale.z);
        }

        // Set the text
        if (barText.text != value.ToString() + "/" + maxValue.ToString())
            barText.text = value.ToString() + "/" + maxValue.ToString();
    }
}
