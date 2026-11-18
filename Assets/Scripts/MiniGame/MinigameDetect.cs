using UnityEngine;

public class MinigameDetect : MonoBehaviour
{
    [SerializeField] private bool isReady = false;
    [SerializeField] private MinigameSample sample;
    [SerializeField] private int count;
    private void Update()
    {
        if (isReady && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(MinigameManager.Instance.StartMiniGame(sample, count));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MG"))
        {
            isReady = true;
            sample = collision.GetComponent<MinigameObject>().sample;
            count = collision.GetComponent<MinigameObject>().count;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MG"))
        {
            isReady = false;
            sample = null;
            count = 0;
        }
    }
}
