using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoSingleton<GameManager>
{


    public void Init()
    {
        
    }

    public void SetStage()
    {
        var datas = Resources.Load<InGameDatas>("ScriptableObject/InGameData");
        InGameData.isLive = true;
        InGameData.isGameSuccess = false;

        InGameData.killCount = 0;
        InGameData.clearEnemyCount = datas.EnemyKillCount;
        InGameData.coolTime = 10;
    }

    public void GameEndTimeStop(int _time)
    {
        Time.timeScale = _time;
    }
}
