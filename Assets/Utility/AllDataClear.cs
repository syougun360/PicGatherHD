using UnityEngine;
using System.Collections;

#if UNITY_METRO && !UNITY_EDITOR
using LegacySystem.IO;
#else
using System.IO;
#endif

public class AllDataClear : MonoBehaviour {

    /// <summary>
    /// フォルダー自体を削除
    /// </summary>
    public void Delete()
    {
        
#if UNITY_METRO && !UNITY_EDITOR
        LibForWinRT.FolderDelete();
#else
        
        if (!Directory.Exists(Application.persistentDataPath + "/../")) return;
        Directory.Delete(Application.persistentDataPath + "/../", true);
#endif
    }
}
