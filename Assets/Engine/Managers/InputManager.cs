using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {   
        if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Keypad1))
            GameManager.combatSpeed = 0.01f;
        if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Keypad2))
            GameManager.combatSpeed = 0.05f;
        if (Input.GetKeyUp(KeyCode.Alpha3) || Input.GetKeyUp(KeyCode.Keypad3))
            GameManager.combatSpeed = 0.2f;
        if (Input.GetKeyUp(KeyCode.Alpha0) || Input.GetKeyUp(KeyCode.Keypad0) ||
            Input.GetKeyUp(KeyCode.Space))
        {
            // paused
            if (GameManager.combatSpeed < 0.0001f)
            {
                GameManager.combatSpeed = GameManager.oldCombatSpeed;
            }
            else
            {
                GameManager.oldCombatSpeed = GameManager.combatSpeed;
                GameManager.combatSpeed = 0f;
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
    }
}