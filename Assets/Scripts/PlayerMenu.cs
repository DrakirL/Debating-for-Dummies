using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour
{
    public void ResearchOpposition()
    {
        PlayerPrefs.SetInt("SubjectOrOpposition", 1);
        Begin();
    }

    public void ResearchSubject()
    {
        PlayerPrefs.SetInt("SubjectOrOpposition", 0);
		Begin();
    }

    public void HundredPercent()
    {
    	PlayerPrefs.SetInt("Sanity", 100);
    	PlayerPrefs.SetInt("Reputation", 100);
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Begin()
    {
        SceneManager.LoadScene("Debate");
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("Sanity", 50);
        PlayerPrefs.SetInt("Reputation", 50);
        PlayerPrefs.SetInt("SubjectOrOpposition", -5);
    }
}
