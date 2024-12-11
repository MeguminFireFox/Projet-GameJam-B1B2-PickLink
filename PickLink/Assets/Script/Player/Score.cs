using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Role _role;
    [SerializeField] public int Point { get; set; } = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Point += 1;

            if (_role.Picsou)
            {
                Point += 2;
            }

            Destroy(other.gameObject);
        }
    }
}
