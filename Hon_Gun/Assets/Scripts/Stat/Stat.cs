using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int hp;
    [SerializeField]
    protected int maxHp;
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected float attackSpeed;

    public virtual int HP { get { return hp; } set { hp = value;  } }
    public virtual int MaxHP { get { return maxHp; } set { maxHp = value; } }
    public virtual float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } } 
    public virtual int Damage { get { return damage; } set {  damage = value; } }
    public virtual float AttackSpeed { get { return attackSpeed; } set {  attackSpeed = value; } }
}
