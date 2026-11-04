using UnityEngine;

public class LittleTextSpeechDetector : MonoBehaviour
{
    public bool IsReady { get; private set; }

    public LittleTextSpeechGraph LTS;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LTS"))
        {
            IsReady = true;
            LTS = collision.GetComponent<LittleTextSpeechObject>().graph;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LTS"))
        {
            IsReady = false;
            LTS = null;
        }
    }
}
