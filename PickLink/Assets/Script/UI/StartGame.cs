using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject _panelDesactive;
    [SerializeField] private GameObject _groupPanelPres;
    [SerializeField] private List<PlayerStun> _listStun;

    public void OnStartGame()
    {
        Time.timeScale = 1.0f;
        _panelDesactive.SetActive(false);
        _groupPanelPres.SetActive(true);

        for (int i = 0; i < _listStun.Count; i++)
        {
            _listStun[i].Stun(false);
        }
    }

    public void OnStartGame2()
    {
        _groupPanelPres.SetActive(false);

        for (int i = 0; i < _listStun.Count; i++)
        {
            _listStun[i].Stun(true);
        }
    }

    private void Update()
    {
        PlayerStun[] stuns = FindObjectsOfType<PlayerStun>();

        foreach (PlayerStun stun in stuns)
        {
            if (!_listStun.Contains(stun))
            {
                _listStun.Add(stun);
            }
        }
    }
}
