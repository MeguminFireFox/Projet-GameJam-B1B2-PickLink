using System.Collections;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [field : SerializeField] public float HP {  get; set; }
    [field : SerializeField] public Animator Animator;
    [SerializeField] private EnemyPatrol _enemyPatrol;

    void Update()
    {
        if (HP <= 0)
        {
            Animator.SetBool("IsMort", true);
            _enemyPatrol.enabled = false;
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        Animator.SetBool("IsMort", false);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Animator.SetBool("IsAttack", true);
            StartCoroutine(WaitAnimation());
        }
    }

    IEnumerator WaitAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        Animator.SetBool("IsAttack", false);
    }
}
