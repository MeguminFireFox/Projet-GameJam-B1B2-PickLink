using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Middle : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;

    public GameObject MiddlePoint;

    private void Update()
    {
        MiddlePoint.transform.position = Vector3.Lerp(Player1.transform.position, Player2.transform.position, 0.5f);
    }
}
