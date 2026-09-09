using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DialogueName", menuName = "Dialogues/Create New Dialogue")]
[Serializable]
public class DialoguesGraph : ScriptableObject
{
    public List<DialoguesNode> Nodes = new List<DialoguesNode>();
}
