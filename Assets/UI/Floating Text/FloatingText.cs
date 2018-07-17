using UnityEngine;

public class FloatingText : MonoBehaviour {
    public Animator animator;

    void Start()
    {
        //destroy at the end of the animation
        Destroy(gameObject, animator.GetCurrentAnimatorClipInfo(0)[0].clip.length - 0.2f);
    }
}
