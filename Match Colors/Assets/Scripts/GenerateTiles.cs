using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GenerateTiles : MonoBehaviour
{
    [SerializeField]
    GameObject Tile;

    // Colors should be
    // 4 - 14
    [SerializeField]
    Color[] Colors = new Color[0];

    private GameObject[,] tiles = new GameObject[0, 0];

    // TODO Refactor this shit to auto generate
    private int[] indexes = new int[0];

    void Start()
    {
        int colorLength = Colors.Length;

        indexes = CreateIndexes(colorLength);
        tiles = new GameObject[colorLength / 2, 4];
        GameManager.tiles = tiles;

        Generate();
    }

    public static int[] CreateIndexes(int Length)
    {
        // 16 tiles -> 8 colors
        int size = Length / 2 + 1;

        int[] tempArray = new int[Length * 2];

        for (int i = 0; i < Length * 2; i++)
        {
            tempArray[i] = i / 2;
        }

        return tempArray;
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
