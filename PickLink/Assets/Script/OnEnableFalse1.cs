using System.Collections;
using UnityEngine;

public class OnEnableFalse1 : MonoBehaviour
{
    [SerializeField] private float _time;

    private void OnEnable()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(_time);
        gameObject.SetActive(false);
    }
}
