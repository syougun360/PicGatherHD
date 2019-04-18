/// ---------------------------------------------------
/// date ： 2015/02/19  
/// brief ： 画像のアニメーション
/// author ： Yamada Masamistu
/// ---------------------------------------------------
/// 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ImageAnimator : MonoBehaviour
{

    [SerializeField]
    List<Sprite> CharacterSprite = new List<Sprite>();

    [SerializeField]
    float ChangeTime = 0.5f;

    [SerializeField]
    bool IsReversePlayBack = false;

    Image CharacterImage = null;
    float Count = 0;
    int Index = 0;
    bool IsReverse = false;


	// Use this for initialization
	void Start () {
        CharacterImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

        Count += Time.deltaTime;
        if (Count >= ChangeTime)
        {
            Count = 0;
            if (IsReverse)
            {
                ReverseIndex();
            }
            else
            {
                AddIndex();
            }
            CharacterImage.sprite = CharacterSprite[Index];
        }
	}

    /// <summary>
    /// Indexを再生
    /// </summary>
    void AddIndex()
    {
        if (!IsRangeOver())
        {
            Index++;
        }
    }

    /// <summary>
    /// Indexを逆再生
    /// </summary>
    void ReverseIndex()
    {
        if (!IsReverseRangeOver())
        {
            Index--;
        }
    }


    /// <summary>
    /// Indexの範囲制御
    /// </summary>
    bool IsRangeOver()
    {
        if (Index >= CharacterSprite.Count - 1)
        {
            if (IsReversePlayBack)
            {
                IsReverse = true;
            }
            else
            {
                Index = 0;
            }
            return true;
        }

        return false;
    }

    /// <summary>
    /// Indexの範囲制御
    /// </summary>
    bool IsReverseRangeOver()
    {
        if (Index <= 0)
        {
            IsReverse = false;
            return true;
        }

        return false;
    }
}
