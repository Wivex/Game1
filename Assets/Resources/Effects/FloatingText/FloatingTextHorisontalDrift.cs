using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// UNDONE: make it AnimationBehaviour
public class FloatingTextHorisontalDrift : MonoBehaviour
{
    Vector3 targetOffsetX;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // -1 or 0 possible
        var sign = Random.Range(-1, 1);
        targetOffsetX = sign < 0 ? new Vector3(Random.Range(-50, -25), 0, 0) : new Vector3(Random.Range(25, 50), 0, 0);
    }

    // LateUpdate instead of Update to override animation positioning
    void LateUpdate()
    {
        transform.position += Vector3.Lerp(Vector3.zero, targetOffsetX, animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }
}