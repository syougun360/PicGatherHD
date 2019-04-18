using UnityEngine;
using System.Collections;
using System;

public class DateTimeController : MonoBehaviour {

    static public DateTime NowTime{get;private set;}

    struct TimeZoneData
    {
        public TimeZoneData(string name,int startTime, int endTime):this()
        {
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
        }

        public string Name { get; private set; }
        int StartTime;
        int EndTime;

        public bool IsTime 
        {
            get
            {
                if ("Sleep" == Name)
                {
                    if (NowTime.Hour <= EndTime || NowTime.Hour >= StartTime)
                    {
                        NowTimeZone = Name;
                        return true;
                    }
                }
                else
                {
                    if (NowTime.Hour >= StartTime && NowTime.Hour <= EndTime)
                    {
                        NowTimeZone = Name;
                        return true;
                    }
                }
                return false;
            }
        }
    }

    static TimeZoneData Morning = new TimeZoneData("Morning",6, 10);
    static TimeZoneData Noon = new TimeZoneData("Noon", 11, 15);
    static TimeZoneData Night = new TimeZoneData("Night", 16, 21);
    static TimeZoneData Sleep = new TimeZoneData("Sleep", 22, 5);

    static string NowTimeZone = string.Empty;
    static string OldTimeZone = string.Empty;

    static public bool IsChanged { get; private set; }

    /// <summary>
    /// 朝
    /// </summary>
    static public bool IsMorning
    {
        get { return Morning.IsTime; }
    }
    

    /// <summary>
    /// お昼
    /// </summary>
    static public bool IsNoon
    {
        get { return Noon.IsTime; }
    }

    /// <summary>
    /// 夜
    /// </summary>
    static public bool IsNight
    {
        get { return Night.IsTime; }
    }

    /// <summary>
    /// お休み中
    /// </summary>
    static public bool IsSleep
    {
        get { return Sleep.IsTime; }
    }

	// Use this for initialization
	void Awake () {
        NowTime = DateTime.Now;
        var a = IsMorning;
        a = IsNoon;
        a = IsNight;
        a = IsSleep;
	}

    void Start()
    {
        OldTimeZone = NowTimeZone;
    }
	
	// Update is called once per frame
	void Update () {

        IsChanged = false;
        if (NowTimeZone != OldTimeZone && !IsChanged)
        {
            OldTimeZone = NowTimeZone;
            IsChanged = true;
        }
	}

}
