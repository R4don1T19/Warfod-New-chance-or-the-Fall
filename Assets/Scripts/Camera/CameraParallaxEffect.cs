using UnityEngine;
public class CameraParallaxEffect : MonoBehaviour
{
    [SerializeField] private GameObject CameraObj;
    [SerializeField] private float ParallaxParameter;
    private Vector2 CameraStartPosition;
    private Vector2 TargetStartPosition;
    private bool FirstFrame = false;
    private void Start()
    {
        CameraObj = Camera.main.gameObject;
        TargetStartPosition = transform.position;
    }
    // Вместо LateUpdate делаем подписку
    private void OnEnable()
    {
        // Application.OnBeforeRender Вызывается тогда, когда нужно сделать что-то непосредственно перед рендером сцены(следующего кадра).
        // В моем случае, он помог мне нубрать проблему с несоответствием вычислениями LateUpdate и данными, которые не успели обновиться.
        Application.onBeforeRender += Parallax;
    }
    private void Parallax()
    {
        if (!FirstFrame)
        {
            TargetStartPosition = transform.position;
            CameraStartPosition = CameraObj.transform.position;
            FirstFrame = true;
            return;
        }

        float ParallaxCoefficient = 1 - ParallaxParameter;

        Vector2 CameraDistance = new Vector2(CameraObj.transform.position.x - CameraStartPosition.x, CameraObj.transform.position.y - CameraStartPosition.y);
        Vector2 distX = new Vector2(TargetStartPosition.x + (CameraDistance.x * ParallaxCoefficient), TargetStartPosition.y + (CameraDistance.y * ParallaxCoefficient));

        transform.position = distX;
    }
    private void OnDisable()
    {
        Application.onBeforeRender -= Parallax;
    }
}
