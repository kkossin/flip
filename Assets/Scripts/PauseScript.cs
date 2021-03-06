﻿using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour
{
    bool Pause = false;

    void Update()
    {
        var Paused = FindObjectOfType<Deathmenu>();

        if (Pause == false)
        {
            Time.timeScale = 1;
        }

        else
        {
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Pause == true)
            {
                Pause = false;
                Paused.HideButtons();
            }

            else
            {
                Pause = true;
                Paused.ShowButtons();
            }
        }
    }
}