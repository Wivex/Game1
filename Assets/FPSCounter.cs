using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public bool enable;

    int FramesPerSec;
    string fps;

    void Start()
    {
        if (enable) StartCoroutine(FPS());
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 100, 10, 150, 20), fps);
    }

    IEnumerator FPS()
    {
        while (enable)
        {
            var lastFrameCount = Time.frameCount;
            var lastTime = Time.realtimeSinceStartup;
            // 1s update pause
            yield return new WaitForSeconds(1);
            var timeSpan = Time.realtimeSinceStartup - lastTime;
            var frameCount = Time.frameCount - lastFrameCount;
            fps = $"FPS: {frameCount / timeSpan:F0}";
        }
    }
}