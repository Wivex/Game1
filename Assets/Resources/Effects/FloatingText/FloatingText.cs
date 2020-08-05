using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// UNDONE: make it AnimationBehaviour
public class FloatingText : MonoBehaviour
{
    internal string text;
    internal Color color;
    internal Image background;

    // flow offset position of X
    Vector3 targetOffsetPosX;
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        text = GetComponent<TextMeshProUGUI>().text;

        // -1 or 0 possible
        var sign = Random.Range(-1, 1);
        targetOffsetPosX = sign < 0 ? new Vector3(Random.Range(-50, -25), 0, 0) : new Vector3(Random.Range(25, 50), 0, 0);
    }

    // LateUpdate instead of Update to override animation positioning
    void LateUpdate()
    {
        transform.position += Vector3.Lerp(Vector3.zero, targetOffsetPosX, animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }
}