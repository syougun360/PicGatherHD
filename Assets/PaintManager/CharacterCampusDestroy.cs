using UnityEngine;
using System.Collections;

/// <summary>
/// ゲームオブジェクトを削除する機能
/// </summary>

public class CharacterCampusDestroy : MonoBehaviour
{

    public void Des()
    {
        var campus = GameObject.Find("CharacterCampus");
        if (campus == null) return;
        Destroy(campus);
    }
}
