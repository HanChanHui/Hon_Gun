using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class Enemy : Entity
{
    [SerializeField]
    private EnemysType enemysType;
    [SerializeField]
    private States state = States.None;
    protected EnemyStat stat;


    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float attackRange = 35f;
    [SerializeField]
    private float bulletTime;

    private void Start()
    {
        WorldHp();
        TimeReset();
        PlayerTrace();
    }

    private void Update()
    {
       switch (state)
       {
            case States.Move:
                TypeMovement(enemysType);
                break;
            case States.Hit:
                //OnDamaged();
                break;
            case States.Die:
                OnDead();
                break;
            case States.None:
                PlayerTrace();
                break;
       }
    }

    protected override void Init()
    {
        stat = GetComponent<EnemyStat>();
        agent = GetComponent<NavMeshAgent>();

        stat.EnemyType = enemysType;
        stat.MaxHP = Data.MaxHp;
        stat.HP = Data.MaxHp;
        stat.MoveSpeed = Data.MoveSpeed;
        stat.Damage = Data.Damage;
        stat.AttackSpeed = Data.AttackSpeed;
        stat.AttackRange = attackRange;
        stat.Reloading = bulletTime;
    }

    private void WorldHp()
    {
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
        {
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
        }
    }

}
