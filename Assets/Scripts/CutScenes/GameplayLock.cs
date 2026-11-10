using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    // Передаем список шагов напрямую, а не корутину уничтожаемого объекта!
    public void PlayCutscene(List<CutSceneStep> steps)
    {
        if (!IsBusy)
        {
            StartCoroutine(RunCutscene(steps));
        }
    }

    private IEnumerator RunCutscene(List<CutSceneStep> steps)
    {
        IsBusy = true;
        if (PlayerBaseMovement.Instance != null) PlayerBaseMovement.Instance.enabled = false;
        if (CameraBehavior.Instance != null) CameraBehavior.Instance.CameraBindedToDialogue = true;
        if (LTSd != null) LTSd.gameObject.SetActive(false);

        IconsUIEventTrigger.OnChangeIconUI("CutScene");

        // Выполняем шаги катсцены здесь — в объекте, который НЕ уничтожается при смене сцены!
        foreach (var step in steps)
        {
            if (step != null)
            {
                yield return step.Run();
            }
        }

        Debug.Log("CutScene is over!");

        if (PlayerBaseMovement.Instance != null) PlayerBaseMovement.Instance.enabled = true;
        if (LTSd != null) LTSd.gameObject.SetActive(true);
        if (CameraBehavior.Instance != null)
        {
            CameraBehavior.Instance.CameraBindedToDialogue = false;
            CameraBehavior.Instance.CameraInCutscene = false;
        }

        IsBusy = false;
        IconsUIEventTrigger.OnChangeIconUI("Empty");
    }

    // Метод для принудительного сброса блокировки (вызовем его перед загрузкой сцены)
    public void ResetLockOnSceneChange()
    {
        StopAllCoroutines(); // Останавливаем текущую катсцену, так как локация меняется
        IsBusy = false;
    }
}
