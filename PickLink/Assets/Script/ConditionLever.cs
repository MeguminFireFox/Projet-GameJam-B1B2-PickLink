using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConditionLever : MonoBehaviour
{
    private static ConditionLever Instance;
    public static ConditionLever instance 
    {  
        get 
        { 
            if (Instance == null)
            {
                Instance = FindAnyObjectByType<ConditionLever>();
            }
            return Instance; 
        } 
    }


    [SerializeField]
    List<GameObject> Lever = new List<GameObject>();

    int ObjectOpen = 0;

    bool CanOpen;

    Vector3 StartPosition;

    [SerializeField]
    Transform FinalPosition;

    public void Start()
    {
        StartPosition = gameObject.transform.position;
    }

    public void CheckToOpen()
    {
        foreach (GameObject go in Lever)
        {
            if (go != null)
            {
                if (go.GetComponent<Lever>().IsActive == true)
                {
                    ObjectOpen++;
                }
                else if (go.GetComponent<Lever>().IsActive == false)
                {
                    ObjectOpen--;
                }
            }
        }

        if (ObjectOpen == Lever.Count)
        {
            CanOpen = true;
        }
        else
        {
            CanOpen = false;
        }
    }

    public void Update()
    {
        if (CanOpen == true)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, FinalPosition.position, 1 * Time.deltaTime);
        }
        else
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, StartPosition, 1 * Time.deltaTime);
        }
    }
}
