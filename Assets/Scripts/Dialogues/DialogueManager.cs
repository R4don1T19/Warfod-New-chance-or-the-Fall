using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Player;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public bool IsPlaying { get; private set; }
    public bool TimeToEndDialogue { get { return timeToEndDialogue; } }
    [SerializeField] private GameObject DialogueBoxUI;
    [SerializeField] private TMP_Text SpeakerName;
    [SerializeField] private TMP_Text SpeakerSpeech;
    [SerializeField] private Image FrolanImage;
    [SerializeField] private Image OpponentImage;
    private bool timeToEndDialogue = false;
    private int NodeCount = 0;
    private int LineCount = 0;
    internal Coroutine TypeCoroutine;
    private string PreviousSpeakerName;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
    private void Start()
    {
        DialogueBoxUI.SetActive(false);
    }
    public IEnumerator PlayDialogue(DialoguesGraph dialogue)
    {
        DialogueBoxUI.SetActive(true);
        PreviousSpeakerName = null;

        foreach(DialoguesNode node in dialogue.Nodes)
        {
            foreach(DialoguesLine line in node.Lines)
            {
                IsPlaying = true;
                SpeakerName.text = line.name;
                OpponentImage.sprite = line.OpponentSprite;
                FrolanImage.sprite = line.FrolanSprite;

                yield return TypeTextLine(line.line, line.FirstLine);
                PreviousSpeakerName = line.name;
                // этот метод как yeild return WaitForSecond, но с постановкой условия
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            }
        }
        DialogueBoxUI.SetActive(false);
        IsPlaying = false;
    }
    public void StartDialogue(DialoguesGraph dialogue)
    {
        CameraBehavior.Instance.CameraBindedToDialogue = true;
        PlayerBaseMovement.Instance.enabled = false;
        DialogueBoxUI.SetActive(true);

        DialoguesNode Node = dialogue.Nodes[NodeCount];
        DialoguesLine Line = Node.Lines[LineCount];

        SpeakerName.text = Line.name;
        OpponentImage.sprite = Line.OpponentSprite;
        FrolanImage.sprite = Line.FrolanSprite;
        bool firstLine = Line.FirstLine;

        // Проверка на принудительную остановку корутины
        if (TypeCoroutine != null)
            StopCoroutine(TypeCoroutine);
        TypeCoroutine = StartCoroutine(TypeTextLine(Line.line, firstLine));

        // нужно для корутины
        PreviousSpeakerName = SpeakerName.text;

        LineCount++;
        if (LineCount >= Node.Lines.Count)
        {
            LineCount = 0;
            NodeCount++;
            if (NodeCount >= dialogue.Nodes.Count)
            {
                timeToEndDialogue = true;
            }
        }
    }
    private IEnumerator TypeTextLine(string line, bool FirstLine)
    {
        if (PreviousSpeakerName == SpeakerName.text && !FirstLine)
            SpeakerSpeech.text += " ";
        else
            SpeakerSpeech.text = "";

        foreach (char c in line)
        {
            SpeakerSpeech.text += c;
            yield return new WaitForSeconds(0.03f);
        }

        //TypeCoroutine = null; лучше временно убрать(по подсказке)
    }
    public void EndDialogue()
    {
        LineCount = 0;
        NodeCount = 0;
        DialogueBoxUI.SetActive(false);
        timeToEndDialogue = false;

        PlayerBaseMovement.Instance.enabled = true;
        CameraBehavior.Instance.CameraBindedToDialogue = false;
    }
}
