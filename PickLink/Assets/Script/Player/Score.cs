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

    IEnumerator ActiveQuota()
    {
        yield return new WaitForSeconds(0.1f);
        switch (_role.RoleName)
        {
            case "Picsou":
                Quota = ListQuota[0];
                _role.Objectif = "Ramasser le plus de Pieces";
                break;
            case "Killer":
                Quota = ListQuota[1];
                _role.Objectif = "Tuer le plus d'Ennemis";
                break;
            case "Voltigeur":
                Quota = ListQuota[2];
                _role.Objectif = "Sauter le plus de fois";
                break;
            case "Glouton":
                Quota = ListQuota[3];
                _role.Objectif = "Ramasser le plus de Viande";
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
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        _invincibility = true;
        yield return new WaitForSeconds(3);
        _invincibility = false;
    }
}
