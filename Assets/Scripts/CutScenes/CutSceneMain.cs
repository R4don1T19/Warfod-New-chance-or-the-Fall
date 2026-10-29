using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CutSceneMain : MonoBehaviour
{
    [SerializeField] private List<CutSceneStep> steps;
    private bool played = false;
    private IEnumerator RunSteps()
    {
        foreach (var step in steps)
            yield return step.Run();
        Debug.Log("CutScene is over!");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (played || !collision.CompareTag("Player"))
            return;
        played = true;
        GameplayLock.Instance.Play(RunSteps());
    }
}
