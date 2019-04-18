/// ---------------------------------------------------
/// date ： 2015/01/20 
/// brief ： 絵を描く背景の描画コントローラー
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DrawingCampusBackGroundController : MonoBehaviour
{

    Image BackGround = null;

	// Use this for initialization
	void Start () {
        BackGround = GetComponent<Image>();

	}
	
    /// <summary>
    /// 使用可能(描画する)
    /// </summary>
    public void Enabled()
    {
        BackGround.enabled = true;
    }

    /// <summary>
    /// 使用不可(描画しない)
    /// </summary>
    public void Unavailable()
    {
        BackGround.enabled = false;
    }
}
