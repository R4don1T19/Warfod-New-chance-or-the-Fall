using UnityEngine;

public class LocationsDoorID : MonoBehaviour
{
    [Header("ID назвать так: НазваниетекущейcценыФактическоеназвание")]
    [SerializeField] private string fromID;
    public string FromID { get { return fromID; } }
    [SerializeField] private string toID;
    public string ToID { get { return toID; } }
    [SerializeField] private string sceneToLoad;
    public string SceneToLoad { get { return sceneToLoad; } }
}
