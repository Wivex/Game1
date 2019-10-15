using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// UNDONE: make it AnimationBehaviour
public class RandomPositionOffset : MonoBehaviour
{
    Vector3 targetPos;
    float curPercent, increment;

    // Start is called before the first frame update
    void Start()
    {
        var curAnim = GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).FirstOrDefault().clip;
        var totalAnimFrames = curAnim.frameRate * curAnim.length;
        increment = 1f / totalAnimFrames;

        // -1 or 0 possible
        var sign = Random.Range(-1, 1);
        targetPos = sign < 0 ? new Vector3(Random.Range(-50, -25), 0, 0) : new Vector3(Random.Range(25, 50), 0, 0);
    }

    // LateUpdate instead of Update to override animation positioning
    void LateUpdate()
    {
        transform.position += Vector3.Lerp(Vector3.zero, targetPos, curPercent);
        curPercent += increment;
    }
}