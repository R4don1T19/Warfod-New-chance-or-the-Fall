using UnityEngine;

public class MinigameDetect : MonoBehaviour
{
    [SerializeField] private bool isReady = false;
    [SerializeField] private MinigameNewsPaper newspaper;
    [SerializeField] private GameObject CutSceneToOn;

    private void Update()
    {
        if (isReady && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(MinigameManager.Instance.StartMiniGame(newspaper, CutSceneToOn));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MG"))
        {
            isReady = true;
            CutSceneToOn = collision.GetComponent<MinigameObject>().CutSceneToOn;
            newspaper = collision.GetComponent<MinigameObject>().newspaper;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MG"))
        {
            isReady = false;
            newspaper = null;
            CutSceneToOn = null;
        }
    }
}