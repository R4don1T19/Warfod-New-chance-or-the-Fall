using System.Collections;
using UnityEngine;

public class CameraFadeEffectCutscene : CutSceneStep
{
    [SerializeField] float speed = 1.0f;
    [Header("1 - непрозрачный, 0 - прозрачный")]
    [SerializeField] float TargetAlpha;
    public override IEnumerator Run()
    {
        yield return CameraFadeEffect.Instance.FadeEffect(TargetAlpha, speed); ;
        Debug.Log("FadeEffect is over");
    }
}
