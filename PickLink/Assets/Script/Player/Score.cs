using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Role _role;
    [SerializeField] public int Point { get; set; } = 0;
    [SerializeField] public int Quota { get; set; }
    [SerializeField] private List<int> _listQuota;
    private bool _invincibility = false;
    [SerializeField] public int CoinCount { get; set; }
    private bool _quota = false;

    
    void Start()
    {
        switch(_role.RoleName)
        {
            case "Picsou":
                Quota = _listQuota[0];
                break;
            case "Killer":
                Quota = _listQuota[1];
                break;
            case "Voltigeur":
                Quota = _listQuota[2];
                break;
            case "Glouton":
                Quota = _listQuota[3];
                break;
        }
    }
    
    void Update()
    {
        if (_quota) return;

        if (CoinCount >= Quota)
        {
            Point += 100;
            _quota = true;
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
