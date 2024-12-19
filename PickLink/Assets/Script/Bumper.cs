using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField]
    float bounceForce = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.layer ==3)
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up*bounceForce, ForceMode.Impulse);
        }
    }
}