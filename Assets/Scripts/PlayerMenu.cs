using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour
{
    public void ResearchOpposition()
    {
        PlayerPrefs.SetInt("SubjectOrOpposition", 1);
        SceneManager.LoadScene("Debate");
    }

    public void ResearchSubject()
    {
        PlayerPrefs.SetInt("SubjectOrOpposition", 0);
        SceneManager.LoadScene("Debate");
    }
}
