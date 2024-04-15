using Consts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{

    [SerializeField]
    private GameObject player;
    public GameObject GetPlayer() => player;

    private HashSet<GameObject> enemy = new HashSet<GameObject>();

    public GameObject Spawn(WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case WorldObject.None:
                break;
            case WorldObject.Enemy:
                enemy.Add(go);
                break;
            case WorldObject.Player:
                player = go;
                break;
        }

        return go;

    }


    public WorldObject GetWorldObjectType(GameObject go)
    {
        WorldObject type = go.GetComponent<Entity>().type;

        if (go == null)
            return WorldObject.None;

        return type;

    }

    public void Despawn(GameObject go, float time = 0)
    {
        WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case WorldObject.Player:
                if (player == go)
                {
                    player = null;
                }
                break;
            case WorldObject.Enemy:
                {
                    if (enemy.Contains(go))
                    {
                        enemy.Remove(go);
                        inGameData.killCount += 1;
                    }
                }
                break;
        }
        Managers.Resource.Destroy(go, time);
    }

    private void ClearEnemy()
    {
        if (enemy == null) return;
        foreach (var _enemy in enemy)
        {
            Destroy(_enemy);
        }
    }

    public void Clear()
    {
        ClearEnemy();
        enemy.Clear();
        Despawn(player);
    }


}
