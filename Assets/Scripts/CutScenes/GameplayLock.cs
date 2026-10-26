using UnityEngine;
using System.Collections;

public class GameplayLock : MonoBehaviour
{
    public static GameplayLock Instance;
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

        yield return routine;

        PlayerBaseMovement.Instance.enabled = true;
        CameraBehavior.Instance.CameraBindedToDialogue = false;
        IsBusy = false;
    }
}
