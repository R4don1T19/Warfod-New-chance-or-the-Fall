using System.Collections;
using TMPro;
using UnityEngine;

public class LittleTextSpeechManager : MonoBehaviour
{
    public static LittleTextSpeechManager Instance;
    [SerializeField] private TMP_Text textField;
    [SerializeField] private GameObject textFieldGameObject;
    [SerializeField] private LittleTextSpeechDetector LTSd;
    private Coroutine TypeCoroutine;
    private bool isReady = false;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && LTSd.IsReady && TypeCoroutine == null)
            TypeCoroutine = StartCoroutine(StartLTS(LTSd.LTS, LTSd.LTSObject));
    }
    public IEnumerator StartLTS(LittleTextSpeechGraph LTSlocal, GameObject LTSObject = null, bool isReaction = false)
    {
        textFieldGameObject.SetActive(true);
        IconsUIEventTrigger.OnChangeIconUI("LTS");
        if (!isReaction)
        PlayerBaseMovement.Instance.enabled = false;
        try
        {
            foreach (LittleTextSpeechLine node in LTSlocal.Graph)
            {
                yield return StartCoroutine(TypeTextLine(node.Speech));
                if (isReady == true)
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            }
        }
        finally
        {
            isReady = false;
            if (!isReaction)
            {
                PlayerBaseMovement.Instance.enabled = true;
                LTSObject.SetActive(false);
            }
            IconsUIEventTrigger.OnChangeIconUI("Empty");
            textFieldGameObject.SetActive(false);
            TypeCoroutine = null; //лучше временно убрать(по подсказке
        }
    }

    private IEnumerator TypeTextLine(string line)
    {
        textField.text = "";
        foreach (char c in line)
        {
            textField.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        isReady = true;
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.E));
    }
}
