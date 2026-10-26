using System.Collections;
using UnityEngine;

public class DialogueStep : CutSceneStep
{
    [SerializeField] private DialoguesGraph dialogue;

    public override IEnumerator Run()
    {
        yield return DialogueManager.Instance.PlayDialogue(dialogue);
    }
}
