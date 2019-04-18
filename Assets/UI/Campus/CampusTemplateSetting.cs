
/// ---------------------------------------------------
/// date ： 2015/01/22 
/// brief ： キャンパスのテンプレートを設定する
/// author ： Yamada Masamistu
/// ---------------------------------------------------
/// 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CampusTemplateSetting : MonoBehaviour {

    [SerializeField]
    Sprite NonSelectSprite = null;

    [SerializeField]
    GameObject FrontFace = null;

    Image TemplateImage = null;
    public bool IsSelect { get; private set; }

	// Use this for initialization
	void Start () {
        TemplateImage = GetComponent<Image>();
        NonSelect();
	}

    /// <summary>
    /// Spriteを設定する。
    /// </summary>
    /// <param name="templateSprite"></param>
    public void SetSprite(Sprite templateSprite)
    {
        TemplateImage.sprite = templateSprite;
        IsSelect = true;
        NonActiveFace();
    }
    
    /// <summary>
    /// Spriteを設定する。
    /// </summary>
    /// <param name="templateSprite"></param>
    public void SetDoubleSprite(Sprite templateSprite)
    {
        TemplateImage.sprite = templateSprite;
        FrontFace.SetActive(true);
        IsSelect = true;
    }
    

    /// <summary>
    /// 選ばれているフラグを消す。
    /// </summary>
    public void NonSelect()
    {
        TemplateImage.sprite = NonSelectSprite;
        IsSelect = false;
        NonActiveFace();
    }

    /// <summary>
    /// 顔を非アクティブにする
    /// </summary>
    void NonActiveFace()
    {
        if (!FrontFace.activeSelf) return;
     
        FrontFace.SetActive(false);
    }
}
