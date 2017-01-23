using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    private Image Bar;

	// Use this for initialization
	void Start ()
    {
	    Bar = GetComponent<Image>();
	}

    public void SetValue(int value)
    {
        Bar.fillAmount = ((float)value / 100.0f);
    }
}
