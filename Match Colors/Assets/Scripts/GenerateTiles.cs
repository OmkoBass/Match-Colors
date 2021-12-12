using UnityEngine;
using System.Linq;

public class GenerateTiles : MonoBehaviour
{
    [SerializeField]
    GameObject Tile;

    [SerializeField]
    Color[] Colors = new Color[14];

    private GameObject[,] tiles = new GameObject[7, 4];

    // TODO Refactor this shit to auto generate
    private int[] indexes = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13 };

    void Start()
    {
        Generate();
    }

    private void Generate()
    {
        int step = 0;

        // Shuffle index array
        indexes = indexes.OrderBy(x => Random.Range(0, indexes.Length)).ToArray();

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                var newTile = Instantiate(Tile);

                tiles[i, j] = newTile;
                tiles[i, j].transform.position = new Vector2(i * 2.5f, j * 2.5f);

                var Color = tiles[i, j].transform.Find("Color");
                SpriteRenderer spriteRenderer = Color.GetComponent<SpriteRenderer>();

                spriteRenderer.color = Colors[indexes[step]];

                step++;
            }
        }

        GameManager.tiles = tiles;
    }

    public void Reset()
    {
        int step = 0;

        indexes = indexes.OrderBy(x => Random.Range(0, indexes.Length)).ToArray();

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                var Color = tiles[i, j].transform.Find("Color");
                SpriteRenderer spriteRenderer = Color.GetComponent<SpriteRenderer>();

                spriteRenderer.color = Colors[indexes[step]];

                step++;
            }
        }
    }
}
