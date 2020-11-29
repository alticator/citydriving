using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private bool paused;
    public Text textField;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                paused = false;
                Time.timeScale = 1;
                textField.text = "";
            }
            else
            {
                paused = true;
                Time.timeScale = 0;
                textField.text += "PAUSED";
            }
        }
    }
}
