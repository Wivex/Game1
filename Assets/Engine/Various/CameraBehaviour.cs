using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class CameraBehaviour : MonoBehaviour
{
    public int minZoomSize, maxZoomSize;
    public Text text, text2, text3, text4;

    float baseCamOrthoSize, curCamSizeFactor;
    int panRefFingerId;

    Vector3 panRefPos, targetPos;

    void Awake()
    {
        baseCamOrthoSize = Camera.main.orthographicSize;
        curCamSizeFactor = Camera.main.orthographicSize / baseCamOrthoSize;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            HandleTouch();
        }
        else
        {
            panRefFingerId = -1;
            HandleMouse();
        }



        //// accepts finger touch as mouse click
        //if (Input.GetMouseButtonDown(0))
        //{
        //    // save first touching finger as pan controlling one
        //    if (Input.touchCount > 0)
        //        //panRefFingerId = Input.GetTouch(0).fingerId;

        //    panRefPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //}
        //if (Input.GetMouseButton(0))
        //{
        //    PanCamera(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //}

        //// handle 2 touch
        //if (Input.touchCount == 2)
        //{
        //    var touchFirst = Input.GetTouch(0);
        //    var touchSecond = Input.GetTouch(1);

        //    var touchFirstOld = touchFirst.position - touchFirst.deltaPosition;
        //    var touchSecondOld = touchSecond.position - touchSecond.deltaPosition;

        //    var distOld = (touchFirstOld - touchSecondOld).magnitude;
        //    var distCur = (touchFirst.position - touchSecond.position).magnitude;

        //    var zoomOffset = distCur - distOld;

        //    // multitouch zoom
        //    ZoomCamera(zoomOffset, 0.2f);
        //}
        //    ZoomCamera(Input.GetAxis("Mouse ScrollWheel"), 200);
    }

    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // save ref position on mouse click down
            panRefPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            // pan camera relative to saved ref position
            PanCamera(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        // zoom on mouse wheel
        ZoomCamera(Input.GetAxis("Mouse ScrollWheel"), 200);
    }

    void HandleTouch()
    {
        //text.text = $"touch pos {Input.GetTouch(0).position}";
        //text2.text = $"world touch pos {Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)}";
        //text3.text = $"mouse pos {Input.mousePosition}";
        //text4.text = $"world mouse pos {Camera.main.ScreenToWorldPoint(Input.mousePosition)}";
        if (panRefFingerId != Input.GetTouch(0).fingerId)
        {
            panRefFingerId = Input.GetTouch(0).fingerId;
            panRefPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        else
            PanCamera(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));

        // handle 2 touch
        if (Input.touchCount == 2)
        {
            var touchFirst = Input.GetTouch(0);
            var touchSecond = Input.GetTouch(1);

            var touchFirstOld = touchFirst.position - touchFirst.deltaPosition;
            var touchSecondOld = touchSecond.position - touchSecond.deltaPosition;

            var distOld = (touchFirstOld - touchSecondOld).magnitude;
            var distCur = (touchFirst.position - touchSecond.position).magnitude;

            var zoomOffset = distCur - distOld;

            // multitouch zoom
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
        targetPos = transform.position;

        var maxX = Screen.width / 2f * (1 - curCamSizeFactor);
        var minX = -maxX;
        var maxY = Screen.height / 2f * (1 - curCamSizeFactor);
        var minY = -maxY;

        // restrict possible out of bounds situation
        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        // reassign copied value (can't change by reference)
        transform.position = targetPos;
    }

    void ZoomCamera(float offset, float speed)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - offset * speed, minZoomSize, maxZoomSize);
        // update curCamSizeFactor;
        curCamSizeFactor = Camera.main.orthographicSize / baseCamOrthoSize;
        // additional check of allowed camera position after zoom
        ClampCameraPosition();
    }
}