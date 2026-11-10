using UnityEngine;
using System.Collections.Generic;

public class CutSceneMain : MonoBehaviour
{
    [SerializeField] private List<CutSceneStep> steps;
    private bool played = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Если GameplayLock уже занят ДРУГОЙ катсценой, или эта уже играла — игнорируем
        if (played || GameplayLock.Instance.IsBusy || !collision.CompareTag("Player"))
            return;

        played = true;
        GameplayLock.Instance.PlayCutscene(steps);
    }
}
