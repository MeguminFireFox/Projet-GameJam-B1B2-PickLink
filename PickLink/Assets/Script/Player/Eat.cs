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

    void Update()
    {
        if (_quota) return;

        if (Lunch >= _score.Quota)
        {
            _score.Point += 100;
            _quota = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            _score.Point += 1;
            transform.localScale += new Vector3(_foodInfluence, _foodInfluence, _foodInfluence);
            _transformJump.position = new Vector3(transform.position.x, _foodInfluence + 0.5f, transform.position.z);

            if (_role.RoleName == "Glouton")
            {
                Lunch += 1;
                _score.Point += 2;
                transform.localScale += new Vector3(_foodInfluence * _gloutonPlus, _foodInfluence * _gloutonPlus, _foodInfluence * _gloutonPlus);
                _transformJump.position = new Vector3(transform.position.x, _foodInfluence + 0.5f, transform.position.z);
            }

            Destroy(other.gameObject);
        }
    }
}
