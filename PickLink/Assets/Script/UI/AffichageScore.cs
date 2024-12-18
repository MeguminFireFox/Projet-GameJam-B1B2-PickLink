using System.Collections.Generic;
using System.Collections;
using System.Linq;
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
    [SerializeField] private GameObject _panelImpostorWin;
    [SerializeField] private List<int> _listQuotaObjectif;
    private List<string> _trueListString = new List<string>();
    private int _player = 0;
    private int _impostor;
    private List<PlayerStun> _listNotImpostor = new List<PlayerStun>();
    private List<bool> _listBool = new List<bool>();
    private bool _start = false;

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

        if (!_start) return;

        if (_listID[_impostor].CurrentQuota >= _listID[_impostor].Quota)
        {
            for (int i = 0; i < _listNotImpostor.Count; i++)
            {
                if (_listNotImpostor[i].Torpeur >= _listNotImpostor[i]._torpeurObjectif)
                {
                    _listBool[i] = true;
                }
                else
                {
                    _listBool[i] = false;
                }
            }

            if (_listBool.All(b => b))
            {
                _panelImpostorWin.SetActive(true);
                Time.timeScale = 0f;
            }
        }
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
        for (int i = 0; i < _listQuotaObjectif.Count; i++)
        {
            _listID[_player].ListQuota.Add(_listQuotaObjectif[i]);
        }
        string role = _listString[Random.Range(0, _listString.Count)];
        _listIDRole[_player].RoleName = role;
        _trueListString.Add(role);
        _listString.Remove(role);
    }

    public void OnActualisation()
    {
        for (int i = 0; i < _listID.Count; i++)
        {
            _text[i].text = $"{_trueListString[i]} Quota :\n {_listID[i].CurrentQuota} / {_listID[i].Quota}";

            if (_listID[i].CurrentQuota >= _listID[i].Quota && _listID[i].CurrentQuota >= 1)
            {
                _text[i].color = Color.green;
            }
        }
    }

    public void OnStart()
    {
        _impostor = Random.Range(0, _listIDRole.Count);
        Role role = _listIDRole[_impostor];
        role.Fou = true;

        for (int i = 0; i < _listID.Count; i++)
        {
            if (i != _impostor)
            {
                _listNotImpostor.Add(_listID[i].GetComponent<PlayerStun>());
                _listBool.Add(false);
            }
        }
        StartCoroutine(WaitStart());
    }

    IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(5);
        _start = true;
    }
}
