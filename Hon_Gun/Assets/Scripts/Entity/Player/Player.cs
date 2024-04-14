using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Entity
{

    protected PlayerStat stat;

    private void Start()
    {
        InputEvent();
        HPEvent();
    }

    private void OnEnable()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        
    }


    protected override void Init()
    {
        stat = GetComponent<PlayerStat>();

        stat.MaxHP = Data.MaxHp;
        stat.HP = Data.MaxHp;
        stat.MoveSpeed = Data.MoveSpeed;
        stat.Damage = Data.Damage;
        stat.AttackSpeed = Data.AttackSpeed;
    }

}
