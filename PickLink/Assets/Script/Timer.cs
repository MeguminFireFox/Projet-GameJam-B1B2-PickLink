using UnityEngine;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    public int minutes = 2;
    public int seconds = 0;
    private float countdownTime;

    private bool isTimerRunning = false;

    public GameObject DeathPanel;

    public TextMeshProUGUI timerText;

    void Awake()
    {
        countdownTime = (minutes * 60) + seconds;
    }

    public void StartTimer() //Je pensais mettre cette partie dans le bouton à la fin quand le joueur découvre leurs rôles
    {
        if (!isTimerRunning)
        {
            StartCoroutine(TimerCoroutine());
        }
    }

    private IEnumerator TimerCoroutine()
    {
        isTimerRunning = true;

        float timeRemaining = countdownTime;

        while (timeRemaining > 0)
        {
            int minutesLeft = Mathf.FloorToInt(timeRemaining / 60);
            int secondsLeft = Mathf.FloorToInt(timeRemaining % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutesLeft, secondsLeft);

            yield return new WaitForSeconds(1f);

            timeRemaining -= 1f;
        }

        timerText.text = "00:00";
        isTimerRunning = false;
        DeathPanel.SetActive(true);
    }
}
