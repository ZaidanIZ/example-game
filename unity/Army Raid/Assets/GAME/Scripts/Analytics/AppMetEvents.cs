/*
 * Version for Unity
 * © 2015-2020 YANDEX
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * https://yandex.com/legal/appmetrica_sdk_agreement/
 */

using UnityEngine;
using System.Collections.Generic;

public class AppMetEvents : MonoBehaviour
{
    //private AudioSource _audioSource;
    //private static AppMetEvents _instance;
    //public static AppMetEvents Instance { get { return _instance; } }

    //private Dictionary<string, object> eventParameters = new Dictionary<string, object>();

    //private void Awake()
    //{
    //    if (_instance != null && _instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        _instance = this;
    //    }
    //    _audioSource = GetComponent<AudioSource>();
    //    _audioSource.Play();
    //    DontDestroyOnLoad(gameObject);
    //}

    //public void LevelStartEvent(int _level, int _gameCount, string type)
    //{
    //    eventParameters["level_number"] = _level.ToString();
    //    eventParameters["level_count"] = _gameCount.ToString();
    //    eventParameters["level_random"] = "1";
    //    eventParameters["level_type"] = type;
    //    AppMetrica.Instance.ReportEvent("level_start", eventParameters);
    //    eventParameters.Clear();
    //}

    //public void LevelFinishEvent(int _level, int _gameCount, string type, string _result, float _gametimer, int _progress)
    //{
    //    eventParameters["level_number"] = _level.ToString();
    //    eventParameters["level_count"] = _gameCount.ToString();
    //    eventParameters["level_random"] = "1";
    //    eventParameters["level_type"] = type;       
    //    eventParameters["result"] = _result;  
    //    eventParameters["time"] = _gametimer.ToString("f0");
    //    eventParameters["progress"] = _progress.ToString("f0");
    //    // eventParameters["continue"] = Spawn.ContinueCounter.ToString();
    //    AppMetrica.Instance.ReportEvent("level_finish", eventParameters);
    //    eventParameters.Clear();
    //}
}


