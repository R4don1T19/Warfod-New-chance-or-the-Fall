using System.Collections;
using UnityEngine;

public class WaitStep : CutSceneStep
{
    [SerializeField] private float Waitness = 1f;

    public override IEnumerator Run()
    {
        yield return new WaitForSeconds(Waitness);
        Debug.Log("Waitness is over");
    }
}
