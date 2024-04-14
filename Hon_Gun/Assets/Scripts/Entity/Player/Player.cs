using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Entity
{

    protected PlayerStat stat;

    private void Start()
    {
        InputEvent();
        WorldHp();
    }

    private void Update()
    {
        Attack();
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

    private void WorldHp()
    {
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
        {
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
        }
    }

}
