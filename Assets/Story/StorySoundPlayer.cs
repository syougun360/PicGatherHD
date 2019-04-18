using UnityEngine;
using System.Collections;

public class StorySoundPlayer : MonoBehaviour {

    [SerializeField]
    BGMPlayer Player = null;

    [SerializeField]
    string ResName = string.Empty;

    [SerializeField]
    FadeTimeData FadeData = new FadeTimeData(1,1);

	void Start () {
        Player.Play(ResName, FadeData);
	}

    /// <summary>
    /// 停止する
    /// </summary>
    public void Stop()
    {
        Player.Stop();
    }
}
