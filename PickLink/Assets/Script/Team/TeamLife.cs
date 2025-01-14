using TMPro;
using UnityEngine;

public class TeamLife : MonoBehaviour
{
    public static TeamLife Instance;
    [field: SerializeField] public int Life { get; set; } = 5;
    [field: SerializeField] public GameObject Center {  get; set; }
    [SerializeField] private GameObject _panelMort;
    [SerializeField] private TMP_Text _text;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        _text.text = $"{Life}";

        if (Life <= 0)
        {
            _panelMort.SetActive(true);
        }

        Score[] score = FindObjectsOfType<Score>();

        if (score.Length == 0) return;

        Center.transform.position = CalculateCenter(score);
    }
    Vector3 CalculateCenter(Score[] transforms)
    {
        if (transforms == null || transforms.Length == 0)
        {
            Debug.LogError("La liste des Transforms est vide ou nulle.");
            return Vector3.zero;
        }

        Vector3 sum = Vector3.zero;

        foreach (Score t in transforms)
        {
            sum += t.transform.position;
        }

        return sum / transforms.Length;
    }

}
