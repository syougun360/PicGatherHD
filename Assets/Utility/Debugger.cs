using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

public class Debugger : MonoBehaviour
{

    public Debugger(string name, int id)
    {
        Name = name;
        ID = id;
        DebugStringData = new List<string>();
    }

    List<string> DebugStringData = null;
    string Name = string.Empty;
    int ID = 0;

    public static class Type
    {
        public const string Begin = "Begin";
        public const string End = "End";
    }

    public void AddFunction(string debugString, string type)
    {
        //DebugStringData.Add(debugString + " " + type);

        //JsonWrite();
    }

    public void AddParameter(string name ,object value)
    {
        //DebugStringData.Add(name + " : " + value.ToString());

        //JsonWrite();
    }

    public void JsonWrite()
    {
        var json = LitJson.JsonMapper.ToJson(DebugStringData);

#if UNITY_METRO && !UNITY_EDITOR
        LibForWinRT.WriteFileText("Debug", Name + ID + ".json", json);
#else
#endif
    }
}
