using UnityEngine;
using UnityEngine.UI;

public class BackgroundRandomiser : MonoBehaviour
{
    [SerializeField]
    Sprite[] Backgrounds;

    private Image background;
    void Start()
    {
        background = GetComponent<Image>();

        int randomInt = Random.Range(0, Backgrounds.Length - 1);

        background.sprite = Backgrounds[randomInt];
    }
}
