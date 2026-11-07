using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class DataForBorders : MonoBehaviour
{
    public float LeftBorder { get { return leftBorder; } }
    [SerializeField] private float leftBorder;
    public float RightBorder { get { return rightBorder; } }
    [SerializeField] private float rightBorder;
    public float UpBorder { get { return upBorder; } }
    [SerializeField] private float upBorder;
    public float DownBorder { get { return downBorder; } }
    [SerializeField] private float downBorder;
}
