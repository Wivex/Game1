using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    float oldTimeScale;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Keypad1))
            Time.timeScale = 0.5f;
        if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Keypad2))
            Time.timeScale = 1f;
        if (Input.GetKeyUp(KeyCode.Alpha3) || Input.GetKeyUp(KeyCode.Keypad3))
            Time.timeScale = 2f;
        if (Input.GetKeyUp(KeyCode.Alpha4) || Input.GetKeyUp(KeyCode.Keypad4))
            Time.timeScale = 5f;
        if (Input.GetKeyUp(KeyCode.Alpha0) || Input.GetKeyUp(KeyCode.Keypad0) ||
            Input.GetKeyUp(KeyCode.Space))
        {
            // paused
            if (Time.timeScale == 0f)
            {
                Time.timeScale = oldTimeScale;
            }
            else
            {
                oldTimeScale = Time.timeScale;
                Time.timeScale = 0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
    }
}