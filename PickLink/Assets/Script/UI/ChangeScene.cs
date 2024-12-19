using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void OnChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
