using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool paused;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                paused = false;
                Time.timeScale = 1;
            }
            else
            {
                paused = true;
                Time.timeScale = 0;
            }
        }
    }
}
