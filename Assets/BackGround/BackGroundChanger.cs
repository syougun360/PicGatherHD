using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackGroundChanger : MonoBehaviour
{
    [SerializeField]
    Texture MorningTexture = null;

    [SerializeField]
    Texture NoonTexture = null;

    [SerializeField]
    Texture NightTexture = null;

    Texture OldTexture = null;

    // Use this for initialization
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        ChangeTexture(DateTimeController.IsMorning, MorningTexture);
        ChangeTexture(DateTimeController.IsNoon, NoonTexture);
        ChangeTexture(DateTimeController.IsNight, NightTexture);
        ChangeTexture(DateTimeController.IsSleep, NightTexture);
    }

    /// <summary>
    /// テクスチャ(背景)を切り替える
    /// </summary>
    /// <param name="IsTime"></param>
    /// <param name="texture"></param>
    void ChangeTexture(bool IsTime, Texture texture)
    {
        if (IsTime && OldTexture != texture)
        {
            renderer.material.mainTexture = texture;
            OldTexture = texture;
        }
    }
}
