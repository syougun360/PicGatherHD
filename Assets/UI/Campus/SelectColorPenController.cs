using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectColorPenController : MonoBehaviour {

    [SerializeField]
    Sprite InitColorPen = null;

    Image PenImage = null;

    public void Init()
    {
        PenImage.sprite = InitColorPen;
    }

    void Start()
    {
        PenImage = GetComponent<Image>();
    }

    public void ChangeColor(Sprite penSprite)
    {
        PenImage.sprite = penSprite;
    }
}
