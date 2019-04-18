using UnityEngine;
using System.Collections;

public class StampSelecter : MonoBehaviour {

    [SerializeField]
    GameObject Frame = null;

    [SerializeField]
    SoundEffectPlayer Player = null;

    [SerializeField]
    string ChangeSoundResName = string.Empty;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    /// <summary>
    /// スタンプが選択されたところに移動
    /// </summary>
    /// <param name="obj"></param>
    public void SelectFrameMove(GameObject obj)
    {
        var buttonRectTrans = obj.GetComponent<RectTransform>();
        var rectTrans = Frame.GetComponent<RectTransform>();
        rectTrans.anchoredPosition3D = buttonRectTrans.anchoredPosition3D;

        Player.Play(ChangeSoundResName);
    }
}
