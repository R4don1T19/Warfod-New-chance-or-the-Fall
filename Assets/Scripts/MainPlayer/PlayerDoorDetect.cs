using UnityEngine;

public class PlayerDoorDetect : MonoBehaviour
{
    internal string ToID;
    internal string FromID;
    internal string SceneToLoad;
    internal bool readyToEnter = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            readyToEnter = true;
            ToID = other.GetComponentInChildren<LocationsDoorID>().ToID;
            FromID = other.GetComponentInChildren<LocationsDoorID>().ToID;
            SceneToLoad = other.GetComponentInChildren<LocationsDoorID>().SceneToLoad;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            readyToEnter = false;
            ToID = null;
            FromID = null;
            SceneToLoad = null;
        }
    }
}
