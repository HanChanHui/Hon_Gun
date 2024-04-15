using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public partial class GameManager
{
    [SerializeField]
    private InGameData inGameData;
    public InGameData InGameData
    {
        get => inGameData;
        set => inGameData = value;
    }
   
}

[System.Serializable]
public class InGameData
{
    public bool isLive = false;
    public bool isGameSuccess = false;

    public int killCount = 0;
    public int clearEnemyCount = 0;
    public int coolTime = 0;

    public void Reset()
    {
        isLive = true;
        isGameSuccess = false;

        killCount = 0;
        clearEnemyCount = 20;
        coolTime = 0;
    }
}
