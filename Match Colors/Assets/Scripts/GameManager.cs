using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int Score = 0;
    public static bool gameOver = false;
    public static GameObject TileOne;
    public static GameObject TileTwo;

    public static GameObject[,] tiles = new GameObject[0, 0];

    public static Color[] Colors = new Color[0];
}
