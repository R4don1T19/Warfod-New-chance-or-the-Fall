using UnityEngine;
using TMPro;
using System.Collections;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private GameObject MiniGameUIObject;
    [SerializeField] private TMP_Text header;
    [SerializeField] private TMP_Text textField;
    [SerializeField] private TMP_Text bottom;
    [SerializeField] private float pauseBeforeReaction;
    public static MinigameManager Instance;
    private bool IsRunning = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
    public IEnumerator StartMiniGame(MinigameNewsPaper newspaper)
    {
        if (IsRunning || newspaper == null) yield break;

        IsRunning = true;
        PlayerBaseMovement.Instance.enabled = false;
        Debug.Log("IS WORKING");
        MiniGameUIObject.SetActive(true);
        try 
        {
            foreach (MinigameSample news in newspaper.newspaper)
            {
                header.text = news.header;
                textField.text = news.text;
                bottom.text = news.bottom;

                yield return null;
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));

                if (news.LTS != null)
                {
                    yield return new WaitForSeconds(pauseBeforeReaction);  // реакция не «выскакивает»
                    yield return LittleTextSpeechManager.Instance.StartLTS(news.LTS, isReaction: true);
                }
            }
        }
        finally
        {
            PlayerBaseMovement.Instance.enabled = true;
            ClearLines();
            IsRunning = false;
            MiniGameUIObject.SetActive(false);
        }
    }
    private void ClearLines()
    {
        header.text = null;
        textField.text = null;
        bottom.text = null;
    }
}
