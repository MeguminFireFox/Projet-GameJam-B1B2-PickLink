using UnityEngine;

public class Eat : MonoBehaviour
{
    [SerializeField] private Role _role;
    [SerializeField] private Score _score;
    [SerializeField] private float _foodInfluence;
    [SerializeField] private int _gloutonPlus;
    [SerializeField] private Transform _transformJump;
    private int _lunch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {

            _lunch += 1;
            transform.localScale += new Vector3(_foodInfluence, _foodInfluence, _foodInfluence);
            _transformJump.position = new Vector3(transform.position.x, _foodInfluence + 0.5f, transform.position.z);

            if (_role.RoleName == "Glouton")
            {
                _score.Point = _lunch;
                transform.localScale += new Vector3(_foodInfluence * _gloutonPlus, _foodInfluence * _gloutonPlus, _foodInfluence * _gloutonPlus);
                _transformJump.position = new Vector3(transform.position.x, _foodInfluence + 0.5f, transform.position.z);
            }

            Destroy(other.gameObject);
        }
    }
}
