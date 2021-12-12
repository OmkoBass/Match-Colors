using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textTimer;

    void Update()
    {
        textTimer.text = $"Score: {GameManager.Score}";
    }
}
