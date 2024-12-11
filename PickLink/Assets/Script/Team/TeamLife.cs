using UnityEngine;

public class TeamLife : MonoBehaviour
{
    public static TeamLife Instance;
    [field: SerializeField] public int Life;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
