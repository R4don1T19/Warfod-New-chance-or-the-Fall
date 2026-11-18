using UnityEngine;
using TMPro;
using System.Collections;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private GameObject MiniGameUIObject;
    [SerializeField] private TMP_Text header;
    [SerializeField] private TMP_Text textField;
    [SerializeField] private TMP_Text bottom;
    public static MinigameManager Instance;

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
    public IEnumerator StartMiniGame(MinigameSample sample, int count)
    {
        PlayerBaseMovement.Instance.enabled = false;
        Debug.Log("IS WORKING");
        MiniGameUIObject.SetActive(true);
        yield return null;

        for (int i = 0; i < count; i++)
        {
            header.text = sample.header;
            textField.text = sample.text;
            bottom.text = sample.bottom;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));

            yield return null;
        }
        ClearLines();
        MiniGameUIObject.SetActive(false);
    }
    private void ClearLines()
    {
        header.text = null;
        textField.text = null;
        bottom.text = null;
    }
}
