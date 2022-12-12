using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float previousTime; 
    public float FallTime = 0.8f;
    public Factory factory;
    public float Speed = 0.01f;
    public int degrees = 90;
    private Vector3 StartPosition;

    private void Start()
    {
        StartPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && CanMoveLeft())
        {
            Left();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && CanMoveRight())
        {
            Right();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && CanDown())
        {
            Down();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Up();
        }

        if (!CanDown())
        {
            End();
        }

        if (Time.time - previousTime > FallTime)
        {
            transform.position += Vector3.down;
            previousTime = Time.time;
        }
    } 

    private bool CanDown()//проверяет можно ли передвигатся вниз
    {
        foreach (Part part in GetComponentsInChildren<Part>())
        {
            if (part.RawDown())
            {
                return false;
            }
        }
        return true;
    }

    private bool CanMoveLeft()//проверяет можно ли передвигатся влево
    {
        foreach (Part part in GetComponentsInChildren<Part>())       
            if (part.RawLeft())
                return false;       
        return true;
    }

    private bool CanMoveRight()//проверяет можно ли передвигатся вправо
    {
        foreach (Part part in GetComponentsInChildren<Part>())        
            if (part.RawRight())           
                return false;                   
        return true;
    }

    public void SetColor(Color color)//даёт цвет объекту 
    {
        foreach(MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
        {            
            meshRenderer.material.color = color;
        }
    }

    private void End()//
    {
        if (StartPosition == transform.position)
        {
            Debug.Log("Loser!");
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Collider>().enabled = true;
            }
            transform.DetachChildren();
            factory.Create();
        }
        GameObject.Destroy(gameObject);
    }

    void Left()//передвигает объект влево
    {
        transform.position += Vector3.left;  
    }
    void Right()//передвигает объект вправо
    {
        transform.position += Vector3.right;
    }
    void Down()//передвигает объект вниз
    {
        transform.position += Vector3.down;
    }
    private void Up()//поварачивает объект на 90 градусов
    {
        var Angle = transform.eulerAngles;
        Angle.z += degrees;
        transform.eulerAngles = Angle;
        foreach (Part part in GetComponentsInChildren<Part>())
        {
            if (part.Validation() == false)
            {
                transform.position -= (Vector3) GetDelta();               
                break;
            }
        }
        
    }

    Vector2 GetDelta()//перемещает объект вниз каждую секунду
    {
        int direction = 0;
        int max = 0;
        foreach(Part part in GetComponentsInChildren<Part>())
        {
            int current =(int) Mathf.Abs(part.transform.position.x) - 4;
            if(max < current)
            {
                max = current;
                direction = (int) Mathf.Sign(part.transform.position.x - 4); 

            }
        }
        return Vector2.right * max * direction;
    }
}
