using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{

    public override int HP 
    { 
        get => base.HP;
        set => base.HP = value; 
    }

    public override int MaxHP 
    { 
        get => base.MaxHP;
        set => base.MaxHP = value;
    }

    public override float MoveSpeed
    {
        get => base.MoveSpeed;
        set => base.MoveSpeed = value;
    }
    public override int Damage
    {
        get => base.Damage; 
        set => base.Damage = value;
    }

    public override float AttackSpeed 
    { 
        get => base.AttackSpeed;
        set => base.AttackSpeed = value;
    }
}
