using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeStep : CutSceneStep
{
    [SerializeField] private string Scenename;

    public override IEnumerator Run()
    {
        LocationsTransit.Instance.CutSceneTransit = true;

        // Перед самой загрузкой очищаем состояние выживающего GameplayLock
        GameplayLock.Instance.ResetLockOnSceneChange();

        // Запускаем обычную загрузку сцены
        SceneManager.LoadScene(Scenename);

        yield return null;
    }
}
