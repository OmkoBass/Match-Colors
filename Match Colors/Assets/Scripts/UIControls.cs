using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class UIControls : MonoBehaviour
{
    [SerializeField]
    AudioClip AudioButtonClick;

    public void StartGame()
    {
        PlayButtonClickSound();

        SceneManager.LoadScene("SceneGame");
    }

    public void ToMainMenu()
    {
        PlayButtonClickSound();

        SceneManager.LoadScene("SceneMainMenu");
    }

    public void Reset()
    {
        PlayButtonClickSound();

        // 4 x 4
        // Length is 4, we multiply it by 2 to get 8, which is the number of colors
        // 5 x 4
        // Length is 5, we multiply it by 2 to get 10, which is the number of colors
        var indexes = GenerateTiles.CreateIndexes(GameManager.tiles.GetLength(0) * 2);

        int step = 0;

        indexes = indexes.OrderBy(x => Random.Range(0, indexes.Length)).ToArray();

        int X = GameManager.tiles.GetLength(0);
        int Y = GameManager.tiles.GetLength(1);

        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                var QuestionMark = GameManager.tiles[i, j].transform.Find("QuestionMark");

                QuestionMark.gameObject.SetActive(true);
                GameManager.tiles[i, j].tag = "Tile";
            }
        }

        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                var Color = GameManager.tiles[i, j].transform.Find("Color");
                SpriteRenderer spriteRenderer = Color.GetComponent<SpriteRenderer>();

                spriteRenderer.color = GameManager.Colors[indexes[step]];

                step++;
            }
        }

        GameManager.Timer = 0;
        GameManager.Score = 0;
        GameManager.TileOne = null;
        GameManager.TileTwo = null;
        GameManager.gameOver = false;
    }

    private void PlayButtonClickSound()
    {
        SoundManager.Instance.PlayMusic(AudioButtonClick);
    }
}
