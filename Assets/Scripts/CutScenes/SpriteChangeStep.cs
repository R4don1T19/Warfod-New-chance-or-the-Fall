using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpriteChangeStep : CutSceneStep
{
    [SerializeField] private Sprite AfterSprite;
    [SerializeField] private SpriteRenderer TargetSprite;

    private void ChangeSprite(Sprite sprite, SpriteRenderer target)
    {
        target.sprite = sprite; 
    }
    public override IEnumerator Run()
    {
        ChangeSprite(AfterSprite, TargetSprite);
        yield return null;
    }
}
