using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistance : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerNeighboor1;

    [SerializeField]
    private GameObject MiddlePoint;

    private LayerMask layerMask;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(GetDistance(gameObject.transform, PlayerNeighboor1.transform) > 10)
        {
            Vector3 direction = MiddlePoint.transform.position - gameObject.transform.position;
            rb.AddForce(direction*2f);
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
