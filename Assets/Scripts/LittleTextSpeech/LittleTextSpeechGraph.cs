using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName = "LITTLETEXTS", menuName = "New LTS")]
public class LittleTextSpeechGraph : ScriptableObject
{
    public List<LittleTextSpeechLine> Graph = new List<LittleTextSpeechLine>();
}
