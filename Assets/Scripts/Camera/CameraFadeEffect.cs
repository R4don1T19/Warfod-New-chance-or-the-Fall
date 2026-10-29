using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraFadeEffect : MonoBehaviour
{
    public static CameraFadeEffect Instance;
    [SerializeField] private Image FadeImage;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }
    public IEnumerator FadeEffect(float targetAlfa, float duration)
    {
        float startAlfa = FadeImage.color.a;
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            float localAlpha = Mathf.Lerp(startAlfa, targetAlfa, time / duration);
            FadeImage.color = new Color(0, 0, 0, localAlpha);
            yield return null;
        }
        FadeImage.color = new Color(0, 0, 0, targetAlfa);
    }
}
