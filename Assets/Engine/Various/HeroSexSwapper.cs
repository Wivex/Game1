using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeroSexSwapper : MonoBehaviour
{
    public Image targetImage;
    public List<Sprite> sprites;
    public SexType heroSex;

    // TODO: move to drawer
    // Runs after the animation rendering
    void LateUpdate()
    {
        var curSpriteIndex = sprites.IndexOf(targetImage.sprite);

        if (heroSex == SexType.Female && curSpriteIndex >= 50)
            targetImage.sprite = sprites[curSpriteIndex - 50];
    }
}