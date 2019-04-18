/// ---------------------------------------------------
/// date ： 2015/01/09      
/// brief ： 光のかけらのエフェクトを処理する
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphicEffectController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> EffectPrefab = new List<GameObject>();

    GameObject Particles;

    float LifeTime = 0;
 
    void Start () {
        CreateChildren();
	}

    /// <summary>
    /// 子オブジェクトをランダムで生成
    /// 親オブジェクトの下に設置ています。
    /// レイヤー設定は、ParticleSystemレイヤーにしてあります。
    /// </summary>
    void CreateChildren()
    {
        var index = Random.Range(0, EffectPrefab.Count);

        Particles = (GameObject)Instantiate(EffectPrefab[index],
            transform.position, EffectPrefab[index].transform.localRotation);

        Particles.transform.parent = transform;

        foreach (Transform child in Particles.transform)
        {
            child.particleSystem.renderer.sortingLayerName = "ParticleSystem";
            LifeTime = child.particleSystem.startLifetime;
        }
    }
	
	// Update is called once per frame
    void Update()
    {
        Destroy(gameObject, LifeTime);
	}
}
