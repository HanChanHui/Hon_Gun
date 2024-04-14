using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity Data", menuName = "Scriptable Object/Entity Data")]
public class EntityData : ScriptableObject
{
    [SerializeField]
    private string entityName;
    public string EntityName => entityName;

    [SerializeField]
    private int maxHp;
    public int MaxHp => maxHp;

    [SerializeField]
    private int damage;
    public int Damage => damage;

    [SerializeField]
    private int moveSpeed;
    public int MoveSpeed => moveSpeed;

    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed => attackSpeed;
}
