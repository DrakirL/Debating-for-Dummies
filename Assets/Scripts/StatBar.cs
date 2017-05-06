using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    private Image Bar;

	// Use this for initialization
	void Awake ()
    {
	    Bar = GetComponent<Image>();
	}

    public IEnumerator ColourFlash(float differance)
    {
        if (differance < 0)
        {
            //black
            Bar.color = new Color32(0, 0, 0, 100);
        }

        else
        {
            //green
            Bar.color = new Color32(0, 255, 0, 100);
        }

        //keep the new colour for a bit and then change back.
        yield return new WaitForSeconds(0.75f);
        Bar.color = new Color32(255, 255, 255, 100);
    }

    public void SetValue(int value)
    {
        //Separate the previous value and the new value
        float previousPercent = Bar.fillAmount;
        float percentage = ((float)value / 100.0f);
        Bar.fillAmount = percentage;

        //flash a colour if the new value is different
        if (previousPercent != percentage)
        {
            StartCoroutine(ColourFlash(percentage - previousPercent));
        }
    }
}
