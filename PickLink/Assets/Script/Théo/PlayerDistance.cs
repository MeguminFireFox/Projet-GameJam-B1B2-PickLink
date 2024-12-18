using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistance : MonoBehaviour
{
    [SerializeField]
    public GameObject PlayerNeighboor1;

    [SerializeField]
    public GameObject MiddlePoint1;

    [SerializeField]
    public GameObject PlayerNeighboor2;

    [SerializeField]
    public GameObject MiddlePoint2;

    private Rigidbody rb;

    public int PlayerDistanceNB = 10;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(GetDistance(gameObject.transform, PlayerNeighboor1.transform) > PlayerDistanceNB)
        {
            Vector3 direction = MiddlePoint1.transform.position - gameObject.transform.position;
            rb.AddForce(direction*2f);
        }

        if (PlayerNeighboor2 != null)
        {
            if (GetDistance(gameObject.transform, PlayerNeighboor2.transform) > PlayerDistanceNB)
            {
                Vector3 direction = MiddlePoint2.transform.position - gameObject.transform.position;
                rb.AddForce(direction * 2f);
            }
        }

    } 

    public float GetDistance(Transform transform1, Transform transform2)
    {
        Vector3 w = transform1.position;
        Vector2 v = transform2.position;

        float Distance = Vector3.Distance(w, v);
        return Distance;
    }
}
