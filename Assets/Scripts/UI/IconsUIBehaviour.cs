using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IconsUIBehaviour : MonoBehaviour
{
    public static IconsUIBehaviour Instance;
    public static Action<string> GetUIIcon;
    [SerializeField] private Image image;
    [SerializeField] private Sprite DialogueIcon;
    [SerializeField] private Sprite LTSIcon;
    [SerializeField] private Sprite CutSceneIcon;
    [SerializeField] private Sprite Empty;
    [SerializeField] private Sprite Button;
    private Coroutine CoroutineUI;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }
    private void SetUIIcon(string icon)
    {
        if (CoroutineUI != null)
            return;

        switch (icon)
        {
            case "Button":
                CoroutineUI = StartCoroutine(ChangeIcon(Button));
                break;
            case "Empty":
                CoroutineUI = StartCoroutine(ChangeIcon(Empty));
                break;
            case "CutScene":
                CoroutineUI = StartCoroutine(ChangeIcon(CutSceneIcon));
                break; 
            case "Dialogue":
                CoroutineUI = StartCoroutine(ChangeIcon(DialogueIcon));
                break;
            case "LTS":
                CoroutineUI = StartCoroutine(ChangeIcon(LTSIcon));
                break;
        }
    }
    private IEnumerator ChangeIcon(Sprite icon)
    {
        float durationTime = 1f;
        float currentTime = 0f;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        image.sprite = icon;

        while (durationTime > currentTime)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, currentTime / durationTime);

            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

            yield return null;
        }
        CoroutineUI = null;
    }    
    private void OnEnable()
    {
        IconsUIEventTrigger.GetIconUI += SetUIIcon;
    }
    private void OnDisable()
    {
        IconsUIEventTrigger.GetIconUI -= SetUIIcon;
    }
}
