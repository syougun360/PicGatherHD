using UnityEngine;
using System.Collections;

public class SunAnnounceManager : MonoBehaviour {

    /// <summary>
    /// 太陽を強調しているアイコンを消す
    /// なお、UIのためキャンバスごと消す
    /// </summary>
    public void DestroyAnnounce()
    {
        var announce = FindObjectOfType(typeof(SunAnnounceScaling)) as SunAnnounceScaling;
        if (!announce) return;

        Destroy(announce.gameObject);
    }
    
}
