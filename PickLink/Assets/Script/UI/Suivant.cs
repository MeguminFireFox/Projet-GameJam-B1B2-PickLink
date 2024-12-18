using TMPro;
using UnityEngine;

public class Suivant : MonoBehaviour
{
    [SerializeField] private GameObject _panelNext;
    [SerializeField] private TMP_Text _text;

    public void Next()
    {
        AffichageScore.Instance.IDPlayer++;
        _text.text = $"Player {AffichageScore.Instance.IDPlayer + 1}";
        _panelNext.SetActive(false);
    }
}
