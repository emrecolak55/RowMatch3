using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class CameraPositioner : MonoBehaviour
{
  
    public void CameraMove(int x , int y)
    {
 
        this.transform.position = new Vector3((x * 0.5f) - 0.5f, (y * 0.5f) - 0.5f, -10f);
        if( x > 5 )
        {
            CameraScaler(x);
        }
        else if( y > 9)
        {
            CameraScaler(y);
        }
    }

    private void CameraScaler(float newSize)
    {
        GetComponent<Camera>().orthographicSize = newSize;
    }
}
