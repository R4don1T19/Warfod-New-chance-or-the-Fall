using System.Collections;
using UnityEngine;

public class CameraBehaviorInCutscene : CutSceneStep
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 2.0f;
    private float FixedZ = -10f;
    private float FixedDistance = 0.05f;
    public override IEnumerator Run()
    {
        Transform cameraTransform = Camera.main.transform;
        CameraBehavior.Instance.CameraBindedToDialogue = false;
        CameraBehavior.Instance.CameraInCutscene = true;
        target.position = new Vector3 (target.position.x, target.position.y, FixedZ);

        while (Vector2.Distance(cameraTransform.position, target.position) > FixedDistance)
        {
            cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, target.position, Time.deltaTime * speed);
            yield return null;
        }
        Debug.Log("Camera moves in CutScene");
    }
}
