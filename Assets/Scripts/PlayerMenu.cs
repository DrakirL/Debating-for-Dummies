﻿using UnityEngine;
using System.Collections;

public class PlayerMenu : MonoBehaviour
{
    void ResearchOpposition()
    {
        PlayerPrefs.SetInt("SubjectOrOpposition", 1);
        //Start the debate scene
    }

    void ResearchSubject()
    {
        PlayerPrefs.SetInt("SubjectOrOpposition", 0);
        //Start the debate scene
    }
}
