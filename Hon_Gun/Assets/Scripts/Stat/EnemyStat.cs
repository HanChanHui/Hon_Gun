using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : Stat
{
   public EnemysType EnemyType { get; set; } = EnemysType.None;


    private float attackRange;
    public float AttackRange
    {
        get => attackRange;
        set => attackRange = value;
    }

    private float reloading;
    public float Reloading
    {
        get => reloading;
        set => reloading = value;
    }
}
