using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;

public class NPCStep : CutSceneStep
{
    [SerializeField] private Transform actor;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 2f;
    public override IEnumerator Run()
    {
        while (Vector2.Distance(actor.position, target.position) > 0.02f)
        {
            float direction = target.position.x - actor.position.x;

            if (direction > -0.01f)
                actor.rotation = Quaternion.Euler(0, 180, 0);
            else
                actor.rotation = Quaternion.identity;

            actor.position = Vector2.MoveTowards(actor.position, target.position, speed * Time.deltaTime);
            yield return null;
        }
    }
}
