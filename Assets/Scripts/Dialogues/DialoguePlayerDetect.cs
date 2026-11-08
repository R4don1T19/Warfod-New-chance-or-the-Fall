using UnityEngine;

public class DialoguePlayerDetect : MonoBehaviour
{
    public GameObject DialogueObject
    {
        get
        {
            if (dialogueObject == null) return null;
            return dialogueObject;
        }
    }
    [SerializeField] private DialoguesGraph CurrentDialogue;
    [SerializeField] private bool ReadyToTalk = false;
    [SerializeField] private GameObject dialogueObject;
    private void Update()
    {
        if (GameplayLock.Instance.IsBusy) return;

        if (DialogueManager.Instance.TimeToEndDialogue)
        {
            if (Input.GetKeyDown(KeyCode.E))
                DialogueManager.Instance.EndDialogue(dialogueObject);
        }
        else if (ReadyToTalk)
        {
            //Пока печатается текст, прерывать корутину нельзя!
            if (DialogueManager.Instance.TypeCoroutine != null)
                return;

            if (Input.GetKeyDown(KeyCode.E))
                DialogueManager.Instance.StartDialogue(CurrentDialogue);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DialogueObject"))
        {
            dialogueObject = collision.gameObject;
            CurrentDialogue = collision.GetComponent<DialogueObject>().Dialogue;
            ReadyToTalk = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DialogueObject"))
        {
            dialogueObject = null;
            CurrentDialogue = null;
            ReadyToTalk = false;
        }
    }
}
