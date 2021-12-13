using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    Button ButtonStart;

    [SerializeField]
    Texture2D CursorIdle;

    [SerializeField]
    Texture2D CursorClicked;

    private CursorControls cursorControls;
    private Camera cameraMain;
    void Start()
    {
        ButtonStart.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("SceneGame");
    }

    private void ChangeCursor(Texture2D cursorType)
    {
        // Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 2);
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }

    private void StartedClick()
    {
        ChangeCursor(CursorClicked);
    }

    private void Endedclick()
    {
        ChangeCursor(CursorIdle);
    }

    private void EnableControls()
    {
        cursorControls.Enable();
    }

    private void DisableControls()
    {
        cursorControls.Disable();
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
