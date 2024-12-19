using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionLever : MonoBehaviour
{
    [SerializeField]
    List<GameObject> Lever = new List<GameObject>();

    int ObjectOpen = 0;

    bool CanOpen;

    Vector3 StartPosition;

    public void Start()
    {
        StartPosition = transform.position;
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
            
        }
    }
}
