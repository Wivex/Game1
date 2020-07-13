using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastTargetsDetector : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
            LogRaycastCollisions();
    }

    void LogRaycastCollisions()
    {
        //Create the PointerEventData with null for the EventSystem, and mouse position
        var mousePED = new PointerEventData(null) {position = Input.mousePosition};
        //Create list to receive all results
        var raycastCollisions = new List<RaycastResult>();
        EventSystem.current.RaycastAll(mousePED, raycastCollisions);
        if (raycastCollisions.Any())
        {
            Debug.Log("Objects detecting raycast:");
            foreach (var raycastResult in raycastCollisions)
                Debug.Log($"<b>{raycastResult.gameObject.name}</b>");
        }
    }
}