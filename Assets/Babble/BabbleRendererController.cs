/// ---------------------------------------------------
/// date ： 2015/01/25 
/// brief ： シャボン玉のMeshの描画のコントローラー
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;

public class BabbleRendererController : MonoBehaviour {

    MeshRenderer Renderer = null;

	// Use this for initialization
	void Start () {
	    Renderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (ModeManager.IsDrawingMode)
        {
            Renderer.enabled = false;
        }
        else
        {
            Renderer.enabled = true;
        }
	}

}
