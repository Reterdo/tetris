using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{

    public bool RawLeft()
    {
        if(transform.position.x == -4)
        {
            return true;
        }
        Debug.DrawRay(transform.position, Vector3.left, Color.red);
        return Physics.Raycast(transform.position, Vector3.left, 1);
    }
    public bool RawRight()
    {
        if(transform.position.x == 4)
        {
            return true;
        }
        Debug.DrawRay(transform.position, Vector3.right, Color.red);
        return Physics.Raycast(transform.position, Vector3.right, 1);
    }
    public bool RawDown()
    {
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, 1);
    }
}

