﻿/// ---------------------------------------------------
/// date ： 2015/01/19  
/// brief ： ビルボード処理
/// author ： Yamada Masamistu
/// ---------------------------------------------------

using UnityEngine;
using System.Collections;

public class BillboardSetting : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back);

        transform.rotation *= Quaternion.Euler(0, 180, 0);

    }
}