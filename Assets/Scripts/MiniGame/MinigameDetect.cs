using UnityEngine;

public class MinigameDetect : MonoBehaviour
{
    [SerializeField] private bool isReady = false;
    [SerializeField] private MinigameNewsPaper newspaper;
    private void Update()
    {
        if (isReady && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(MinigameManager.Instance.StartMiniGame(newspaper));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MG"))
        {
            isReady = true;
            newspaper = collision.GetComponent<MinigameObject>().newspaper;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MG"))
        {
            isReady = false;
            newspaper = null;
        }
    }
}
