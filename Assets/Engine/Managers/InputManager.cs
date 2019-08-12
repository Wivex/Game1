using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {   
        //if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Keypad1))
        //    GameManager.settings.combatSpeed = 0.01f;
        //if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Keypad2))
        //    GameManager.settings.combatSpeed = 0.05f;
        //if (Input.GetKeyUp(KeyCode.Alpha3) || Input.GetKeyUp(KeyCode.Keypad3))
        //    GameManager.settings.combatSpeed = 0.2f;
        //if (Input.GetKeyUp(KeyCode.Alpha0) || Input.GetKeyUp(KeyCode.Keypad0) ||
        //    Input.GetKeyUp(KeyCode.Space))
        //{
        //    // paused
        //    if (GameManager.settings.combatSpeed < 0.0001f)
        //    {
        //        GameManager.settings.combatSpeed = GameManager.settings.oldCombatSpeed;
        //    }
        //    else
        //    {
        //        GameManager.settings.oldCombatSpeed = GameManager.settings.combatSpeed;
        //        GameManager.settings.combatSpeed = 0f;
        //    }
        //}
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
    }
}