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

        int size = colorLength / 2;

        int[] tempArray = new int[colorLength * 2];

        for (int i = 0; i < colorLength * 2; i++)
        {
            tempArray[i] = i / 2;
        }

        // 8 Colors means 16 tiles
        // 16 tiles means it's a 4 x 4 grid
        // So the constant 4 is the 4 columns
        GameObject[,] tempTiles = new GameObject[colorLength / 2, 4];

        for (int i = 0; i < colorLength / 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                tempTiles[i, j] = new GameObject();
            }
        }

        indexes = tempArray;
        tiles = tempTiles;

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
