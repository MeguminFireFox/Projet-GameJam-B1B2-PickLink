using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AffichageScore : MonoBehaviour
{
    public static AffichageScore Instance;

    [SerializeField] private List<Score> _listID;
    [SerializeField] private List<Role> _listIDRole;
    [SerializeField] private List<GameObject> _objectText;
    [SerializeField] private List<TMP_Text> _text;
    [SerializeField] private List<string> _listString;
    private List<string> _trueListString = new List<string>();
    private int _player = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Time.timeScale = 0f;
    }

    void Update()
    {
        FindAndAddPoints();
        OnActualisation();
    }

    private void FindAndAddPoints()
    {
        Score[] points = FindObjectsOfType<Score>();

        foreach (Score point in points)
        {
            if (!_listID.Contains(point))
            {
                _listID.Add(point);
                _listIDRole.Add(point.gameObject.GetComponent<Role>());
                _objectText[_player].SetActive(true);
                RoleAttribute();
                _player += 1;
            }
        }
    }
    
    public void RoleAttribute()
    {
        string role = _listString[Random.Range(0, _listString.Count)];
        _listIDRole[_player].RoleName = role;
        _trueListString.Add(role);
        _listString.Remove(role);
    }

    public void OnActualisation()
    {
        for (int i = 0; i < _listID.Count; i++)
        {
            _text[i].text = $"{_trueListString[i]} : {_listID[i].Point} point";
        }
    }

    public void OnStart()
    {
        Role role = _listIDRole[Random.Range(0, _listIDRole.Count)];
        role.Fou = true;
    }
}
