using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;

public class NPCStep : CutSceneStep
{
    [SerializeField] private Transform actor;
    [SerializeField] private Transform target;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private string Trigger;
    [SerializeField] private float speed = 2f;
    public bool isPlayer;
    public override IEnumerator Run()
    {
        if (isPlayer)
        {
            actor = PlayerBaseMovement.Instance.transform;
            animator = PlayerBaseMovement.Instance.GetComponentInChildren<Animator>();
            sprite = PlayerBaseMovement.Instance.GetComponentInChildren<SpriteRenderer>();
        }
        while (Vector2.Distance(actor.position, target.position) > 0.02f)
        {
            float direction = target.position.x - actor.position.x;
            // Порог для определения направления — чтобы не дёргался на месте
            const float directionThreshold = 0.01f;

            if (direction > directionThreshold)
            {
                // Идём вправо — поворачиваем направо (0 градусов)
                actor.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (direction < -directionThreshold)
            {
                // Идём влево — поворачиваем налево (180 градусов)
                actor.rotation = Quaternion.Euler(0, 180, 0);
            }
            // direction ≈ 0 — ничего не меняем, сохраняем текущий поворот

            actor.position = Vector2.MoveTowards(actor.position, target.position, speed * Time.deltaTime);
            animator.SetBool(Trigger, true);
            yield return null;
        }
        animator.SetBool(Trigger, false);
        Debug.Log("NPCStep is over");
    }
}
