using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Textbox : MonoBehaviour
{
	public float letterPause;
	public Text text;

	// Use this for initialization
	void Start ()
    {
		text.text = "";
	}

    public void Type(string message, BasePolitician sender)
    {
        StartCoroutine(TypeText(message, sender));
    }

	IEnumerator TypeText (string message, BasePolitician sender)
    {
        text.text = "";

		foreach (char letter in message.ToCharArray()) 
		{
			text.text += letter;

			yield return 0;
			yield return new WaitForSeconds (letterPause);
		}

        sender.StopTalking();
	}



}
