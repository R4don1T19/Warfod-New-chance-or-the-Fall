using System.Collections;
using TMPro;
using UnityEngine;

public class LittleTextSpeechManager : MonoBehaviour
{
    public static LittleTextSpeechManager Instance;
    [SerializeField] private TMP_Text textField;
    [SerializeField] private GameObject textFieldGameObject;
    [SerializeField] private LittleTextSpeechDetector LTSd;
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
        if (Input.GetKeyDown(KeyCode.E))
            Debug.Log($"E pressed. LTSd null? {LTSd == null}, IsReady: {LTSd?.IsReady}");

        if (Input.GetKeyDown(KeyCode.E) && LTSd.IsReady)
            StartCoroutine(StartLTS(LTSd.LTS));
    }
    private IEnumerator StartLTS(LittleTextSpeechGraph LTSlocal)
    {
        Debug.Log("DEg");
        textFieldGameObject.SetActive(true);
        PlayerBaseMovement.Instance.enabled = false;
        foreach (LittleTextSpeechLine node in LTSlocal.Graph)
        {
            textField.text = node.Speech;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }
        textFieldGameObject.SetActive(false);
        PlayerBaseMovement.Instance.enabled = true;
    }
}
