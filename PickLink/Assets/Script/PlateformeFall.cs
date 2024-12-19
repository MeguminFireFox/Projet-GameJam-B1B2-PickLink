using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateformeFall : MonoBehaviour
{
    [SerializeField]
    private Vector3 startPosition;
    bool ToFall;

    private void Start()
    {
        startPosition = gameObject.transform.parent.position;
    }

    private void Update()
    {
        if (ToFall)
        {
            transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y - 1*Time.deltaTime, transform.parent.position.z);
        }
        else if (!ToFall)
        {
            if (transform.parent.position != startPosition)
            {
                transform.parent.position = Vector3.Lerp(transform.parent.position, startPosition, 1 * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(2f);
        ToFall = true;
        yield return new WaitForSeconds(10f);
        ToFall = false;
    }
}
