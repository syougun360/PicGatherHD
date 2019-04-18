using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarCollected : EventBase
{

    [SerializeField]
    List<GameObject> stars = new List<GameObject>();

    [SerializeField]
    GameObject basket = null;

    [SerializeField]
    GameObject fairy = null;

    [SerializeField]
    GameObject GraphicEffectPrefab = null;

    FeverManager feverManager;

    // Use this for initialization
    void Start()
    {
        var brightStar = GameObject.Find("BrightStar(Clone)");

        Instantiate(basket);
        Instantiate(fairy);

        GameObject starManager = new GameObject();
        starManager.name = "StarManager";
        
        for (int i = 0; i < 30; i++)
        {
            var star = Instantiate(stars[Random.Range(0,stars.Count)]) as GameObject;
            star.transform.SetParent(starManager.transform);
            star.transform.position = new Vector3(
                Random.Range(-3.0f, 3.0f) + brightStar.transform.position.x,
                Random.Range(11.0f, 17.0f),
                Random.Range(-0.5f, 0.5f) + brightStar.transform.position.z);
        }
        
        feverManager = GameObject.Find("FeverGauge").GetComponent<FeverManager>();

    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.FindGameObjectWithTag("Star")) return;

        Finish();
    }

    /// <summary>
    /// 終了時の処理
    /// </summary>
    protected override void Finish()
    {
        Destroy(GameObject.Find("StarManager"));
        var basketPos = GameObject.Find("Basket(Clone)").transform.position;
        Instantiate(GraphicEffectPrefab, basketPos, Quaternion.identity);
        Destroy(GameObject.Find("Basket(Clone)"));
        feverManager.AddScore(feverManager.MaxFeverScore);
        base.Finish();


    }
}
