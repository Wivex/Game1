using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChildPanelsRemover : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}