using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectFoundEffecter : MonoBehaviour {

    [SerializeField]
    GameObject Prefab = null;

    [SerializeField]
    List<Texture> EffectSprite = new List<Texture>();

    [SerializeField]
    int CreateNum = 10;


    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < CreateNum; i++)
        {
            var clone = (GameObject)Instantiate(Prefab, transform.position, transform.rotation);
            var index = Random.Range(0, EffectSprite.Count);

            clone.transform.parent = transform;
            clone.renderer.material.mainTexture = EffectSprite[index];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
