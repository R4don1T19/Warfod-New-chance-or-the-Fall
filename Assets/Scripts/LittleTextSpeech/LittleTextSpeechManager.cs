using TMPro;
using UnityEngine;

public class LittleTextSpeechManager : MonoBehaviour
{
    [SerializeField] private TMP_Text textField;

    private bool readyToStart = false;
    private LittleTextSpeechGraph LTS;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && readyToStart)
            StartLTS(LTS);
    }
    private void StartLTS(LittleTextSpeechGraph LTSlocal)
    {
        
    }
}
