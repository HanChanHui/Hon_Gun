using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InGame Data", menuName = "Scriptable Object/InGame Data")]
public class InGameDatas : ScriptableObject
{
    [SerializeField]
    private string stageName;
    public string StageName => stageName;

    [SerializeField]
    private int enemyKillCount;
    public int EnemyKillCount => enemyKillCount;

}
