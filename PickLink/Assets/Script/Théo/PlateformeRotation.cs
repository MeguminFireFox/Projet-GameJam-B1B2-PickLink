using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeRotation : MonoBehaviour
{
    public float x;
    public float y;
    public float z;

    public float Time;

    void Start()
    {
        StartCoroutine(Rotate());
    }
    
    IEnumerator Rotate()
    {
        yield return new WaitForSeconds(Time);
        gameObject.transform.Rotate(x, y, z);
        StartCoroutine(Rotate());
    }
}
