using UnityEngine;

public class DialogueObject : MonoBehaviour
{
    public DialoguesGraph Dialogue { get { return dialogue; } }
    [SerializeField] private DialoguesGraph dialogue;
}
