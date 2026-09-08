using UnityEngine;

public class PlayerSurfaceConfig : MonoBehaviour
{
    private static Rigidbody2D.SlideMovement slideConfig;
    public Rigidbody2D.SlideMovement SlideConfig { get { return slideConfig; } }
    private float MaxSlipAngle = 50f;
    private void Start()
    {
        slideConfig = new Rigidbody2D.SlideMovement();
        slideConfig.surfaceAnchor = new Vector2(0, -1); // Притяжение к полу. Если первый нуль по иксу, то по игрику тянет вниз.

        slideConfig.surfaceSlideAngle = MaxSlipAngle; // Значение угла, ПОСЛЕ которого игрок не будет цепляться за поверхность.
                                                      // Если меньше, то все ок, а после уже упадет.

        slideConfig.gravitySlipAngle = MaxSlipAngle; // Значение угла, ПОСЛЕ которого Игрок будет соскальзывать на поверхности
                                                     // Если меньше этого значения, то будет стоять, если больше, то начнет соскальзывать.
    // НО, из-за того, что у меня игра про ходьбу влево-вправо, я могу жестко не фиксировать значения этих двух полей.
    // Мой максимум в игре -- поверхность 45 градусов.
    }
}
