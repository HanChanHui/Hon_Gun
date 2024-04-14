using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{


    [SerializeField]
    private GameData gameData;
    public GameData GameData
    {
        get
        {
            if(gameData == null)
            {
                Create();
            }
            return gameData;
        }
    }

    public void Init()
    {
        Create();
    }


    private void Create()
    {
        gameData = new GameData();
        gameData.isNewGame = true;
    }

    public void SetWeaponData(int _i)
    {
        gameData.equipData = _i;
    }

    public int GetWeaponData() => gameData.equipData;

}
