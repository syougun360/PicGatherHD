using UnityEngine;
using System.Collections;

public class AllTutorialDestroyer : MonoBehaviour
{

    [SerializeField]
    private GameObject SkipButton = null;

    [SerializeField]
    private GameObject EventManager = null;

    private TutorialManager TutorialMngr = null;

    void Start()
    {
        TutorialMngr = FindObjectOfType<TutorialManager>();

    }

    void Update()
    {
        AlreadyAllTutorialEnded();
    }

    private void AlreadyAllTutorialEnded()
    {

        foreach (var flag in TutorialMngr.AlreadyEndedList)
        {
            if (!flag) return;
        }

        FinishTutorial();
    }

    /// <summary>
    /// チュートリアルを終了する
    /// </summary>
    public void FinishTutorial()
    {
        SetActiveEventManager();

        DestroyAllTutorial();
    }

    /// <summary>
    /// チュートリアルで使用したものを全部削除する
    /// </summary>
    private void DestroyAllTutorial()
    {
        var Tutorials = GameObject.FindGameObjectsWithTag("Tutorial");
        foreach (var tutorial in Tutorials)
        {
            Destroy(tutorial);
        }

        Destroy(SkipButton);
    }

    /// <summary>
    /// イベントマネージャをアクティブにする
    /// </summary>
    private void SetActiveEventManager()
    {
        EventManager.gameObject.SetActive(true);
    }


}