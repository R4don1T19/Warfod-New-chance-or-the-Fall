using System.Collections;
using UnityEngine;

public abstract class CutSceneStep : MonoBehaviour
{
    public abstract IEnumerator Run();
}
