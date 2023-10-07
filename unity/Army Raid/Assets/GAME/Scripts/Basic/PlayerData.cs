using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int _playerLevel;

    public int GetLevel => _playerLevel;

    private void Awake()
    {
        _playerLevel = PlayerPrefs.GetInt("_playerLevel");
    }

    public void AddLevel()
    {
        _playerLevel++;
        PlayerPrefs.SetInt("_playerLevel", _playerLevel);
    }
}
