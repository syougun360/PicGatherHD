/// ---------------------------------------------------
/// date ： 2015/01/22  
/// brief ： フィーバーのサウンドコントロール
/// author ： Yamada Masamistu
/// ---------------------------------------------------
/// 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FeverSoundController : MonoBehaviour {

    [SerializeField]
    List<string> ResNameData = new List<string>();

    [SerializeField]
    FadeTimeData FadeTime = new FadeTimeData();

    [SerializeField]
    BGMPlayer Player = null;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

    /// <summary>
    /// 再生
    /// </summary>
    public void Play()
    {
        if (Player.IsPlaying) return;

        var resName = ResNameData[Random.Range(0, ResNameData.Count - 1)];

        Player.Play(resName,FadeTime);
    }
    
    /// <summary>
    /// 停止
    /// </summary>
    public void Stop()
    {
        Player.Stop();
    }

}
