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
        GameObject objectSound = SoundObjectPool4.Instance.GetPooledObject();

        if (objectSound != null)
        {
            objectSound.transform.position = transform.position;
            objectSound.SetActive(true);
        }

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
        GameObject objectSound = SoundObjectPool5.Instance.GetPooledObject();

        if (objectSound != null)
        {
            objectSound.transform.position = transform.position;
            objectSound.SetActive(true);
        }

        yield return new WaitForSeconds(0.1f);
        Animator.SetBool("IsAttack", false);
    }
}
