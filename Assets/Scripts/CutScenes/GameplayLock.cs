using UnityEngine;
using System.Collections;

public class GameplayLock : MonoBehaviour
{
    public static GameplayLock Instance;
    [SerializeField] private LittleTextSpeechDetector LTSd;
    public bool IsBusy { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void Play(IEnumerator routine)
    {
        if (!IsBusy)
        {
            StartCoroutine(Run(routine));
        }
    }
    private IEnumerator Run(IEnumerator routine)
    {
        IsBusy = true;
        PlayerBaseMovement.Instance.enabled = false;
        CameraBehavior.Instance.CameraBindedToDialogue = true;
        LTSd.gameObject.SetActive(false);
        IconsUIEventTrigger.OnChangeIconUI("CutScene");

        yield return routine;

        PlayerBaseMovement.Instance.enabled = true;
        LTSd.gameObject.SetActive(true);
        CameraBehavior.Instance.CameraBindedToDialogue = false;
        CameraBehavior.Instance.CameraInCutscene = false;
        IsBusy = false;
        IconsUIEventTrigger.OnChangeIconUI("Empty");
    }
}
