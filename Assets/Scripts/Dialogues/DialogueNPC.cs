using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    public DialoguesGraph Dialogue { get { return dialogue; } }
    [SerializeField] private DialoguesGraph dialogue;
}
