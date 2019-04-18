using UnityEngine;
using System.Collections;

/// <summary>
/// バスケットのソーティング機能
/// </summary>
public class BasketSortingLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("BasketFront").renderer.sortingLayerName = "BasketFront";
        GameObject.Find("BasketBack").renderer.sortingLayerName = "BasketBack";
	}
}
