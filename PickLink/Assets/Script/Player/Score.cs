using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Role _role;
    [SerializeField] public int Point { get; set; } = 0;
    [field : SerializeField] public int Quota { get; set; }
    [SerializeField] public List<int> ListQuota { get; set; } = new List<int>();
    [field: SerializeField] public int CurrentQuota { get; set; }
    private bool _invincibility = false;
    [SerializeField] public int CoinCount { get; set; }
    private bool _quota = false;
    private bool _canQuota = false;
    [SerializeField] private Animator _animator;
    [SerializeField] private List<GameObject> _listAccesoire;

    IEnumerator ActiveQuota()
    {
        yield return new WaitForSeconds(0.1f);
        switch (_role.RoleName)
        {
            case "Picsou":
                Quota = ListQuota[0];
                _role.Objectif = "Ramasser le plus de Pieces";
                _listAccesoire[2].SetActive(true);
                _listAccesoire[3].SetActive(true);
                break;
            case "Killer":
                Quota = ListQuota[1];
                _role.Objectif = "Tuer le plus d'Ennemis";
                _listAccesoire[5].SetActive(true);
                break;
            case "Voltigeur":
                Quota = ListQuota[2];
                _role.Objectif = "Sauter le plus de fois";
                _listAccesoire[0].SetActive(true);
                _listAccesoire[3].SetActive(true);
                break;
            case "Glouton":
                Quota = ListQuota[3];
                _role.Objectif = "Ramasser le plus de Viande";
                _listAccesoire[1].SetActive(true);
                _listAccesoire[4].SetActive(true);
                break;
        }
        yield return new WaitForSeconds(0.1f);
        _canQuota = true;
    }

    void Start()
    {
        StartCoroutine(ActiveQuota());
    }
    
    void Update()
    {
        if (_canQuota)
        {
            if (_quota) return;

            if (CoinCount >= Quota && _role.RoleName == "Picsou")
            {
                Point += 100;
                _quota = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Point += 1;

            if (_role.RoleName == "Picsou")
            {
                CoinCount += 1;
                CurrentQuota = CoinCount;
                Point += 2;
            }

            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (_invincibility) return;

            TeamLife.Instance.Life -= 1;
            _animator.SetBool("IsAie", true);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        _invincibility = true;
        yield return new WaitForSeconds(0.1f);
        _animator.SetBool("IsAie", false);
        yield return new WaitForSeconds(2.9f);
        _invincibility = false;
    }
}
