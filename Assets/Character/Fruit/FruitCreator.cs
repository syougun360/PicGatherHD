/// ---------------------------------------------------
/// date ： 2015/01/19 
/// brief ： 木の実を生成
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;

public class FruitCreator : MonoBehaviour
{
    [SerializeField]
    GameObject FruitPrefab = null;

    void Start()
    {
    }

    public GameObject Create(Vector3 pos)
    {
        var clone = (GameObject)Instantiate(FruitPrefab, pos, FruitPrefab.transform.rotation);
        clone.transform.parent = transform;
        clone.name = FruitPrefab.name;
        return clone;
    }

}
