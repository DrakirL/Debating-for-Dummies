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

    public void SetValue(int value)
    {
        float percentage = ((float)value / 100.0f);
        Bar.fillAmount = percentage;
    }
}
