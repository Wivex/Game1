using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// UNDONE: make it AnimationBehaviour
public class FloatingText : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI textComp;
    public Image icon;

    // flow offset position of X
    int targetOffsetPosX;
    AnimatorStateMonitor animMon;

    internal static void Create(Transform parentTransform, string text, Color color, Sprite icon = null)
    {
        var floatingText = UIManager.i.prefabs.floatingTextPrefab.InstantiateAndGetComp<FloatingText>(parentTransform);
        floatingText.textComp.text = text;
        floatingText.textComp.color = color;
        floatingText.icon.sprite = icon;
    }

    // Start is called before the first frame update
    void Awake()
    {
        animMon = animator.GetBehaviour<AnimatorStateMonitor>();
        // destroy obj on animation finished
        animMon.AnimationFinished += DestroyThis;
        targetOffsetPosX = Random.Range(-1, 1) < 0 ? Random.Range(-50, -25) : Random.Range(25, 50);
    }

    void Update()
    {
        transform.localPosition = new Vector3(targetOffsetPosX * animator.GetCurrentAnimatorStateInfo(0).normalizedTime, transform.localPosition.y, transform.localPosition.z);
    }

    void DestroyThis(Animator anim) => Destroy(gameObject);
}