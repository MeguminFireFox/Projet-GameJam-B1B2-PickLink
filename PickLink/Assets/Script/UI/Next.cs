using UnityEngine;

public class Next : MonoBehaviour
{
    [SerializeField] private GameObject _panelSuivant;
    [SerializeField] private GameObject _panelTrueStart;
    [SerializeField] private GameObject _panelThis;

    public void Suivant()
    {
        if (AffichageScore.Instance.IDPlayer <= AffichageScore.Instance._listID.Count -1)
        {
            _panelSuivant.SetActive(true);
        }
        else
        {
            _panelTrueStart.SetActive(true);
            _panelThis.SetActive(false);
        }
    }
}
