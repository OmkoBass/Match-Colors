using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    private TextMeshProUGUI textTimer;

    private void Start()
    {
        textTimer = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (!GameManager.gameOver)
        {
            GameManager.Timer += Time.deltaTime;
        }

        textTimer.text = GameManager.Timer.ToString("#.##");
    }
}
