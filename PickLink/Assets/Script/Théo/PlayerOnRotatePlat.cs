using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnRotatePlat : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            collision.gameObject.transform.SetParent(gameObject.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
