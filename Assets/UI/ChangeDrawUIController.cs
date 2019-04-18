/// ---------------------------------------------------
/// date ： 2015/01/20 
/// brief ： UIの描画のON/OFFを変える処理
///          ImageとButtonの処理も消している。
/// author ： Yamada Masamistu
/// ---------------------------------------------------
/// 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeDrawUIController : MonoBehaviour {

    Button ButtonObject = null;
    Image ImageObject = null;

	// Use this for initialization
    void Start()
    {
        ButtonObject = GetComponent<Button>();
        ImageObject = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    /// <summary>
    /// 描画不可にする
    /// </summary>
    public void Unavailable()
    {
        ButtonObject.enabled = false;
        ImageObject.enabled = false;
    }

    /// <summary>
    /// 描画可能にする
    /// </summary>
    public void Enabled()
    {
        ButtonObject.enabled = true;
        ImageObject.enabled = true;
    }
}
