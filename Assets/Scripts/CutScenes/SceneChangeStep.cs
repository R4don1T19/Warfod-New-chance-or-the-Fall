using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeStep : CutSceneStep
{
    [SerializeField] private string Scenename;
    public override IEnumerator Run()
    {
        LocationsTransit.Instance.CutSceneTransit = true;
        SceneManager.LoadScene(Scenename);
        yield return null;
    }
}
