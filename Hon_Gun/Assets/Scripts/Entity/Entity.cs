using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    private EntityData data;
    public EntityData Data { get { return data; } set { data = value; } }

    public WorldObject type = WorldObject.None;


    private void Awake()
    {
        Init();
    }

    public abstract void OnDead();

    public abstract void OnDamaged(int damage, float force = 0);

    protected abstract void Init();
}
