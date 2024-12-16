using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [field : SerializeField] public float HP {  get; set; }

    void Update()
    {
        if (HP <= 0)
        {
           Destroy(gameObject);
        }
    }
}
