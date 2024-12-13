using System.Collections;
using UnityEngine;

public class OnEnableFalse : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private MeshCollider _collider;
    [SerializeField] private Rigidbody _rb;

    private void OnEnable()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.25f);
        _collider.isTrigger = false;
        yield return new WaitForSeconds(_time);
        _collider.isTrigger = true;
        _rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
