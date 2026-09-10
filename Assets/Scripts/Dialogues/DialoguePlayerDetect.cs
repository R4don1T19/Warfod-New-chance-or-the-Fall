using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class DialoguePlayerDetect : MonoBehaviour
{
    public Transform NPCTrasform { get { return npctransform; } }
    [SerializeField] private DialoguesGraph CurrentDialogue;
    [SerializeField] private bool ReadyToTalk = false;
    private Transform npctransform;
    private void Update()
    {
        if (DialogueManager.Instance.TimeToEndDialogue)
        {
            if (Input.GetKeyDown(KeyCode.E))
                DialogueManager.Instance.EndDialogue();
        }
        else if (ReadyToTalk)
        {
            // Пока печатается текст, прерывать корутину нельзя!
            if (DialogueManager.Instance.TypeCoroutine != null)
                return;

            if (Input.GetKeyDown(KeyCode.E))
                DialogueManager.Instance.StartDialogue(CurrentDialogue);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            npctransform = collision.transform;
            CurrentDialogue = collision.GetComponent<DialogueNPC>().Dialogue;
            ReadyToTalk = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            npctransform = null;
            CurrentDialogue = null;
            ReadyToTalk = false;
        }
    }
}
