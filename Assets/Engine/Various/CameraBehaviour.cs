using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class CameraBehaviour : MonoBehaviour
{
    public float maxZoom;

    float baseCamOrthoSize, curZoom, camScale;
    int panRefFingerId;
    Vector3 panRefPos;

    void Awake()
    {
        baseCamOrthoSize = Camera.main.orthographicSize;
        var cameraHeight = baseCamOrthoSize * 2;
        curZoom = Screen.height / cameraHeight;
        camScale = cameraHeight / Screen.height;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            HandleTouch();
        }
        else
        {
            // reset finger ID
            panRefFingerId = -1;
            HandleMouse();
        }
    }

    void HandleMouse()
    {
        // save pan starting position on mouse click down
        if (Input.GetMouseButtonDown(0))
            panRefPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // pan camera relative to saved starting position on while button is pressed
        if (Input.GetMouseButton(0))
            PanCamera(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        // zoom on mouse wheel
        ZoomCamera(Input.GetAxis("Mouse ScrollWheel"), 200);
    }

    void HandleTouch()
    {
        // identify first touch, use only it as pan ref point
        if (panRefFingerId != Input.GetTouch(0).fingerId)
        {
            panRefFingerId = Input.GetTouch(0).fingerId;
            panRefPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        // pan camera relative to saved starting position
        else
            PanCamera(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));

        // handle multi-touch zoom
        if (Input.touchCount == 2)
        {
            var touchFirst = Input.GetTouch(0);
            var touchSecond = Input.GetTouch(1);

            var touchFirstOld = touchFirst.position - touchFirst.deltaPosition;
            var touchSecondOld = touchSecond.position - touchSecond.deltaPosition;

            var distOld = (touchFirstOld - touchSecondOld).magnitude;
            var distCur = (touchFirst.position - touchSecond.position).magnitude;

            var zoomOffset = distCur - distOld;

            ZoomCamera(zoomOffset, 0.2f);
        }
    }

    void PanCamera(Vector3 newPanPosition)
    {
        //Translate the camera position based on the new input position. Invert vector to move camera the opposite way
        TryChangePosition(panRefPos - newPanPosition);
    }

    void TryChangePosition(Vector3 offset)
    {
        transform.position += offset;
        ClampCameraPosition();
    }

    void ClampCameraPosition()
    {
        var targetPos = transform.position;

        var maxX = Screen.width / 2f * (1 - 1 / curZoom) * camScale;
        var minX = -maxX;
        var maxY = Screen.height / 2f * (1 - 1 / curZoom) * camScale;
        var minY = -maxY;

        // restrict possible out of bounds situation
        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        // reassign copied value (can't change by reference)
        transform.position = targetPos;
    }

    void ZoomCamera(float offset, float speed)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - offset * speed,
            baseCamOrthoSize / maxZoom, baseCamOrthoSize);
        // update curZoom;
        curZoom = baseCamOrthoSize / Camera.main.orthographicSize;
        // additional check of allowed camera position after zoom
        ClampCameraPosition();
    }
}