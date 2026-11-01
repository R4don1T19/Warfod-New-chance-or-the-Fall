using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpriteChangeStep : CutSceneStep
{
    [SerializeField] private Sprite AfterSprite;
    [SerializeField] private Image TargetImage;

    private void ChangeSprite(Sprite sprite, Image image)
    {
        image.sprite = sprite; 
    }
    public override IEnumerator Run()
    {
        ChangeSprite(AfterSprite, TargetImage);
        yield return null;
    }
}
