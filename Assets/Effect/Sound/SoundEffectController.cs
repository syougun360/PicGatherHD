/// ---------------------------------------------------
/// date ： 2015/01/09      
/// brief ： サウンドエフェクトを処理する
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundEffectController : MonoBehaviour {

    SoundEffectPlayer Player = null;

    [SerializeField]
    List<string> ResNameData = new List<string>(); 

	void Start () {
        Player = GameObject.FindObjectOfType(typeof(SoundEffectPlayer)) as SoundEffectPlayer;
        AudioPlay();
        Destroy(gameObject);
	}
	
    //  オーディオを再生
    //  ランダムで再生するクリップ指定し、再生
    void AudioPlay()
    {
        var index = Random.Range(0, ResNameData.Count);
        Player.Play(ResNameData[index]);
    }

}
