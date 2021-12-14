using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField]
    Texture2D CursorIdle;

    [SerializeField]
    Texture2D CursorClicked;

    [SerializeField]
    AudioClip AudioClickTile;

    private CursorControls cursorControls;
    private Camera cameraMain;
    private void ChangeCursor(Texture2D cursorType)
    {
        // Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 2);
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }

    private void Start()
    {
        cursorControls.Mouse.Click.started += _ => StartedClick();
        cursorControls.Mouse.Click.performed += _ => Endedclick();
    }

    private void StartedClick()
    {
        ChangeCursor(CursorClicked);
    }

    private void Endedclick()
    {
        ChangeCursor(CursorIdle);
        DetectObject();
    }

    private void EnableControls()
    {
        cursorControls.Enable();
    }

    private void DisableControls()
    {
        cursorControls.Disable();
    }

    private void DetectObject()
    {
        if (!cameraMain)
        {
            return;
        }

        Ray ray = cameraMain.ScreenPointToRay(cursorControls.Mouse.Position.ReadValue<Vector2>());
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Tile")
            {
                SoundManager.Instance.PlayMusic(AudioClickTile);

                if (GameManager.TileOne == null)
                {
                    GameManager.TileOne = hit.collider.gameObject;

                    var QuestionMark = GameManager.TileOne.transform.Find("QuestionMark");
                    QuestionMark.gameObject.SetActive(false);
                }
                else if (GameManager.TileTwo == null)
                {
                    GameManager.TileTwo = hit.collider.gameObject;

                    if (GameManager.TileTwo == GameManager.TileOne)
                    {
                        GameManager.TileTwo = null;
                        return;
                    }

                    var QuestionMark = GameManager.TileTwo.transform.Find("QuestionMark");
                    QuestionMark.gameObject.SetActive(false);

                    GameObject TileOne = GameManager.TileOne;
                    GameObject TileTwo = GameManager.TileTwo;

                    SpriteRenderer spriteColorOne = TileOne.transform.Find("Color").GetComponent<SpriteRenderer>();
                    SpriteRenderer spriteColorTwo = TileTwo.transform.Find("Color").GetComponent<SpriteRenderer>();

                    if (spriteColorOne.color == spriteColorTwo.color)
                    {
                        GameManager.TileOne.tag = "Finished";
                        GameManager.TileTwo.tag = "Finished";

                        GameManager.Score += 1;

                        ResetTiles();
                        CheckGameOver();
                    }
                }
                else
                {
                    ResetQuestionMarks();
                    ResetTiles();
                }
            }
        }
    }

    private void ResetQuestionMarks()
    {
        var QuestionMarkOne = GameManager.TileOne.transform.Find("QuestionMark");
        QuestionMarkOne.gameObject.SetActive(true);

        var QuestionMarkTwo = GameManager.TileTwo.transform.Find("QuestionMark");
        QuestionMarkTwo.gameObject.SetActive(true);
    }

    private void ResetTiles()
    {
        GameManager.TileOne = null;
        GameManager.TileTwo = null;
    }

    private void CheckGameOver()
    {
        for (int i = 0; i < GameManager.tiles.GetLength(0); i++)
        {
            for (int j = 0; j < GameManager.tiles.GetLength(1); j++)
            {
                if (GameManager.tiles[i, j].tag != "Finished")
                {
                    GameManager.gameOver = false;
                    return;
                }
            }
        }

        GameManager.gameOver = true;
    }

    private void Awake()
    {
        cameraMain = Camera.main;
        cursorControls = new CursorControls();
        ChangeCursor(CursorIdle);
        Cursor.lockState = CursorLockMode.Confined;
        EnableControls();
    }
}
