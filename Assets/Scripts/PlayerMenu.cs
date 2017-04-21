﻿using UnityEngine;
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

    public void TakeBreak()
    {

        if (PlayerPrefs.GetInt("Sanity") < 65)
        {
            PlayerPrefs.SetInt("Sanity", 65);
        }

        PlayerPrefs.SetInt("SubjectOrOpposition", -1);
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Begin()
    {
        PlayerPrefs.SetInt("SubjectOrOpposition", 7);
        SceneManager.LoadScene("Debate");
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("Sanity", 50);
        PlayerPrefs.SetInt("Reputation", 50);
    }
}
