using UnityEngine;

public class TrueOrFalse : MonoBehaviour
{
    [SerializeField] private GameObject _object;

    public void TrueFalse(bool value)
    {
        _object.SetActive(value);
    }
}
