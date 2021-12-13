using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    [SerializeField]
    GameObject Tile;

    [SerializeField]
    Color[] Colors = new Color[14];

    private TextMeshProUGUI textTimer;

    [SerializeField]
    Button ButtonReset;

    [SerializeField]
    Button ButtonMainMenu;
    private float timer = 0f;

    private void Start()
    {
        textTimer = GetComponent<TextMeshProUGUI>();

        ButtonReset.onClick.AddListener(Reset);
        ButtonMainMenu.onClick.AddListener(ToMainMenu);
    }

    void Update()
    {
        if (!GameManager.gameOver)
        {
            timer += Time.deltaTime;
        }

        textTimer.text = timer.ToString("#.##");
    }

    private void Reset()
    {
        int[] indexes = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13 };

        int step = 0;

        indexes = indexes.OrderBy(x => Random.Range(0, indexes.Length)).ToArray();

        for (int i = 0; i < GameManager.tiles.GetLength(0); i++)
        {
            for (int j = 0; j < GameManager.tiles.GetLength(1); j++)
            {
                var QuestionMark = GameManager.tiles[i, j].transform.Find("QuestionMark");

                QuestionMark.gameObject.SetActive(true);
                GameManager.tiles[i, j].tag = "Tile";
            }
        }

        for (int i = 0; i < GameManager.tiles.GetLength(0); i++)
        {
            for (int j = 0; j < GameManager.tiles.GetLength(1); j++)
            {
                var Color = GameManager.tiles[i, j].transform.Find("Color");
                SpriteRenderer spriteRenderer = Color.GetComponent<SpriteRenderer>();

                spriteRenderer.color = Colors[indexes[step]];

                step++;
            }
        }

        timer = 0;
        GameManager.Score = 0;
        GameManager.TileOne = null;
        GameManager.TileTwo = null;
        GameManager.gameOver = false;
    }

    private void ToMainMenu()
    {
        SceneManager.LoadScene("SceneMainMenu");
    }
}
