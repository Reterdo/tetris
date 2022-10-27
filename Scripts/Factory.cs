using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public Rule[] rules;
    public GameObject[] prefabs;
    private int random;
    void ExecuteRules()
    {
        foreach (Rule rule in rules)
        {
            rule.ExecuteRule();
        }
    }

    public GameObject Create()
    {
        ExecuteRules();
        random = Random.Range(0, prefabs.Length);
        var gameObject = Instantiate(prefabs[random], transform.position, Quaternion.identity);
        gameObject.SetActive(true);
        gameObject.GetComponent<Controller>().SetColor(Random.ColorHSV());
        return gameObject;
    }
    private void Start()
    {
        Create();
    }
}
