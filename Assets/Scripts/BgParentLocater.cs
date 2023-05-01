using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class BgParentLocater : MonoBehaviour
{
    
    public void MoveBackgroundSquares(int x , int y)
    {
        float cameraposx = (x * 0.5f) - 0.5f; // same calculation with camera
        float cameraposy = (y * 0.5f) - 0.5f;
        transform.position = new Vector3(cameraposx , cameraposy , 0);
    }
}
