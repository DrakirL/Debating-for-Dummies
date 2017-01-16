using UnityEngine;
using System.Collections;

public class PlayerMenu : MonoBehaviour
{
    void Start ()
    {
        PlayerPrefs.SetInt("SubjectOrOpposition", 1);
    }
}
