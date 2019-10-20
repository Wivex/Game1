using System;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public int minZoomSize, maxZoomSize, zoomSpeed, panSpeed;

    Camera cam;
    float baseCamOrthoSize;
    float curCamSizeFactor;

    bool panActive, zoomActive;
    int panFingerId; // Touch mode only

    Vector3 lastCursorPosition;
    Vector2[] lastZoomPositions; // Touch mode only

    void Awake()
    {
        cam = GetComponent<Camera>();
        baseCamOrthoSize = cam.orthographicSize;
    }

    void Update()
    {
        if (Input.touchSupported)
        {
            HandleTouch();
        }
        else
        {
            HandleMouse();
        }
    }

    void HandleTouch()
    {
        switch (Input.touchCount)
        {
            case 1: // Panning
                zoomActive = false;
                // If the touch began, capture its position and its finger ID.
                // Otherwise, if the finger ID of the touch doesn't match, skip it.
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    lastCursorPosition = touch.position;
                    panFingerId = touch.fingerId;
                    panActive = true;
                }
                else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved)
                {
                    PanCamera(touch.position);
                }

                break;
            case 2: // Zooming
                panActive = false;
                Vector2[] newPositions = {Input.GetTouch(0).position, Input.GetTouch(1).position};
                if (!zoomActive)
                {
                    lastZoomPositions = newPositions;
                    zoomActive = true;
                }
                else
                {
                    // Zoom based on the distance between the new positions compared to the 
                    // distance between the previous positions.
                    var newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                    var oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                    var offset = newDistance - oldDistance;

                    ZoomCamera(offset, zoomSpeed);

                    lastZoomPositions = newPositions;
                }

                break;
            default:
                panActive = false;
                zoomActive = false;
                break;
        }
    }

    void HandleMouse()
    {
        // On mouse down, capture it's position.
        // On mouse up, disable panning.
        // If there is no mouse being pressed, do nothing.
        if (Input.GetMouseButtonDown(0))
        {
            panActive = true;
            lastCursorPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            ;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            panActive = false;
        }
        else if (Input.GetMouseButton(0))
        {
            PanCamera(cam.ScreenToWorldPoint(Input.mousePosition));
        }

        // Check for scrolling to zoom the camera
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        zoomActive = true;
        ZoomCamera(scroll, zoomSpeed * 10);
        zoomActive = false;
    }

    void PanCamera(Vector3 newPanPosition)
    {
        if (!panActive)
        {
            return;
        }

        //Translate the camera position based on the new input position. Invert vector to move camera the opposite way
        TryChangePosition(lastCursorPosition - newPanPosition);
    }

    void TryChangePosition(Vector3 offset)
    {
        var targetPos = transform.position;

        var maxX = Screen.width * curCamSizeFactor / 2;
        var minX = -maxX;
        var maxY = Screen.height * curCamSizeFactor / 2;
        var minY = -maxY;

        // restrict possible out of bounds situation
        targetPos.x = Mathf.Clamp(targetPos.x + offset.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y + offset.y, minY, maxY);

        // reassign copied value (can't change by reference)
        transform.position = targetPos;
    }

    void ZoomCamera(float offset, float speed)
    {
        if (!zoomActive || Math.Abs(offset) < Mathf.Epsilon)
        {
            return;
        }

        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - offset * speed, minZoomSize, maxZoomSize);
        // update curCamSizeFactor;
        curCamSizeFactor = cam.orthographicSize / baseCamOrthoSize;
    }
}