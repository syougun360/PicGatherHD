using UnityEngine;
using System.Collections;


/// <summary>
/// Destroyに関する便利関数をまとめるクラスです
/// </summary>
public class Destroyer : MonoBehaviour {
    
    /// <summary>
    /// GameObjectを削除し、同じ場所に同じ角度で新しいGameObjectを生成する
    /// </summary>
    /// <param name="destroyObject">DestroyするGameObject</param>
    /// <param name="createObject">InstantiateするGameObject</param>
    public static void DestroyAndCreateObject(GameObject destroyObject,GameObject createObject)
    {
        Instantiate(createObject, destroyObject.transform.position, destroyObject.transform.rotation);

        Destroy(destroyObject);
    }

    /// <summary>
    /// GameObjectを削除し、指定した場所に同じ角度で新しいGameObjectを生成する
    /// </summary>
    /// <param name="destroyObject">DestroyするGameObject</param>
    /// <param name="createObject">InstantiateするGameObject</param>
    /// <param name="_position">生成する場所</param>
    public static void DestroyAndCreateObject(GameObject destroyObject, GameObject createObject, Vector3 _position)
    {
        Instantiate(createObject, _position, destroyObject.transform.rotation);

        Destroy(destroyObject);
    }

    /// <summary>
    /// GameObjectを削除し、指定した場所に指定した角度で新しいGameObjectを生成する
    /// </summary>
    /// <param name="destroyObject">DestroyするGameObject</param>
    /// <param name="createObject">InstantiateするGameObject</param>
    /// <param name="_position">生成する場所</param>
    /// <param name="_rotation">生成する角度</param>
    public static void DestroyAndCreateObject(GameObject destroyObject,GameObject createObject,Vector3 _position,Quaternion _rotation)
    {
        Instantiate(createObject, _position, _rotation);

        Destroy(destroyObject);
    }

}
