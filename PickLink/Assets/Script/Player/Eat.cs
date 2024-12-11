using UnityEngine;

public class Eat : MonoBehaviour
{
    [SerializeField] private Role _role;
    [SerializeField] private Score _score;
    private int _lunch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {

            _lunch += 1;
            _score.Point += 1;

            if (_role.RoleName == "Glouton")
            {
                _score.Point += 2;
            }

            Destroy(other.gameObject);
        }
    }
}
