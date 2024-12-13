using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject _panelDesactive;

    public void OnStartGame()
    {
        Time.timeScale = 1.0f;
        _panelDesactive.SetActive(false);
    }
}
