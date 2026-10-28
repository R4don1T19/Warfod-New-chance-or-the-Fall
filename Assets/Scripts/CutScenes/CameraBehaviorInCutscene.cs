using System.Collections;
using UnityEngine;

public class CameraBehaviorInCutscene : CutSceneStep
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 1f;
    public override IEnumerator Run()
    {
        CameraBehavior.Instance.CameraBindedToDialogue = false;
        CameraBehavior.Instance.CameraInCutscene = true;

    }
}
