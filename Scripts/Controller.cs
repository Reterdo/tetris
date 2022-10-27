using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //проверка лучей слева и справа 
    //поправить скорость падения кубов.Что бы не зависело от ФПС
    public Rule[] rules;
    public float previousTime; 
    public float FallTime = 0.8f;
    public Factory factory;
    public float Speed = 0.01f;
    public int degrees = 90;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

        if (transform.position.y <= -5 | !CanDown())
        {
            End();
        }
        void ExecuteRules()
        {
            foreach (Rule rule in rules)
            {
                rule.ExecuteRule();
            }
        }

        if (Time.time - previousTime > FallTime)
        {
            ExecuteRules();
            transform.position += Vector3.down;
            previousTime = Time.time;
        }
    }
 

    private bool CanDown()
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

    private bool CanMoveLeft()
    {
        foreach (Part part in GetComponentsInChildren<Part>())       
            if (part.RawLeft())
                return false;       
        return true;
    }

    private bool CanMoveRight()
    {
        foreach (Part part in GetComponentsInChildren<Part>())        
            if (part.RawRight())           
                return false;                   
        return true;
    }

    public void SetColor(Color color)
    {
        foreach(MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
        {
            
            meshRenderer.material.color = color;
        }
    }
    private void End()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Collider>().enabled = true;
        }
        transform.DetachChildren();
        GameObject.Destroy(gameObject);
        factory.Create();
    }

    void Left()
    {
        transform.position += Vector3.left;  
    }
    void Right()
    {
        transform.position += Vector3.right;
    }
    void Down()
    {
        transform.position += Vector3.down;
    }
    private void Up()
    {
        var Angle = transform.eulerAngles;
        Angle.z += degrees;
        transform.eulerAngles = Angle;
    }

}
