using System.Collections;
using UnityEngine;

public class Eat : MonoBehaviour
{
    [SerializeField] private Role _role;
    [SerializeField] private Score _score;
    [SerializeField] private float _foodInfluence;
    [SerializeField] private int _gloutonPlus;
    [SerializeField] private Transform _transformJump;
    [SerializeField] public int Lunch {  get; set; }
    private bool _quota = false;
    private bool _canQuota = false;

    private void Start()
    {
        StartCoroutine(ActiveQuota());
    }

    IEnumerator ActiveQuota()
    {
        yield return new WaitForSeconds(2.5f);
        _canQuota = true;
    }

    void Update()
    {
        if (_canQuota)
        {
            if (_quota) return;

            if (Lunch >= _score.Quota && _role.RoleName == "Glouton")
            {
                _score.Point += 100;
                _quota = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            _score.Point += 1;
            Lunch += 1;

            GameObject objectSound = SoundObjectPool1.Instance.GetPooledObject();

            if (objectSound != null)
            {
                objectSound.transform.position = transform.position;
                objectSound.SetActive(true);
            }

            if (Lunch <= 5)
            {
                transform.localScale += new Vector3(_foodInfluence, _foodInfluence, _foodInfluence);
                _transformJump.position -= new Vector3(0, _foodInfluence, 0);
            }

            if (_role.RoleName == "Glouton")
            {
                _score.CurrentQuota = Lunch;
                _score.Point += 2;

                if (Lunch <= 5)
                {
                    transform.localScale += new Vector3(_foodInfluence * _gloutonPlus, _foodInfluence * _gloutonPlus, _foodInfluence * _gloutonPlus);
                    _transformJump.position -= new Vector3(0, _foodInfluence, 0);
                }
            }

            Destroy(other.gameObject);
        }
    }
}
