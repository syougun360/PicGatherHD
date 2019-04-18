using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StoryBoardAnimator : MonoBehaviour {

    [SerializeField]
    List<Sprite> StorySprite = new List<Sprite>();

    [SerializeField]
    float ChangeTime = 2.0f;

    public bool IsFinish { get; private set; }

    Image StoryImage = null;
    float Count = -1;
    int Index = 0;

	// Use this for initialization
	void Start () {
        StoryImage = GetComponent<Image>();
        StoryImage.sprite = StorySprite[Index];
	    
	}
	
	// Update is called once per frame
    void Update()
    {
        if (IsFinish) return;

        ChangeImage();
	}

    /// <summary>
    /// 画像を切り替える
    /// </summary>
    void ChangeImage()
    {
        Count += Time.deltaTime;
        if (Count >= ChangeTime)
        {
            Count = 0;
            AddIndex(1);

            if (IsNextScene()) return;
            RefreshSprite();
        }
    }

    /// <summary>
    /// 次のシーンに遷移かどうか
    /// </summary>
    bool IsNextScene()
    {
        if (Index >= StorySprite.Count - 1)
        {
            Finish();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void Finish()
    {
        IsFinish = true;
    }

    /// <summary>
    /// Indexを加算する
    /// </summary>
    /// <param name="addValue">加算量</param>
    public void AddIndex(int addValue)
    {
        if (IsFinish) return;

        Count = 0;
        Index += addValue;

        if(IsNextScene())
        {
            Index = StorySprite.Count - 1;
        }
    }

    /// <summary>
    /// Spriteの更新
    /// </summary>
    public void RefreshSprite()
    {
        if (IsFinish) return;

        StoryImage.sprite = StorySprite[Index];
    }
}
