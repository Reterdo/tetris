using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    public const int LeftSide = -4;
    public const int RightSide = 4;
    public const int BottomSide = -5;
    /// <summary>
    /// возврощяет true, если если есть припятствие слева.
    /// </summary>
    /// <returns></returns>
    public bool RawLeft()
    {
        if(transform.position.x <= LeftSide)
        {
            return true;
        }
        Debug.DrawRay(transform.position, Vector3.left, Color.red);
        return Physics.Raycast(transform.position, Vector3.left, 1);
    }
    public bool RawRight()
    {
        if(transform.position.x >= RightSide)
        {
            return true;
        }
        Debug.DrawRay(transform.position, Vector3.right, Color.red);
        return Physics.Raycast(transform.position, Vector3.right, 1);
    }
    public bool RawDown()
    {
        if (transform.position.y <= BottomSide)
        {
            return true;
        }
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, 1);
    }
    public bool Validation() => !(transform.position.x <= LeftSide | transform.position.x >= RightSide | transform.position.y <= BottomSide);
}

