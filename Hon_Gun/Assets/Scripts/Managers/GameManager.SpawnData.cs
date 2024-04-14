using Consts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public partial class GameManager
{

    [SerializeField]
    private GameObject player;
    public GameObject GetPlayer() => player;
    private IEnumerator InGameStart()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        yield return null;
    }

    private HashSet<GameObject> enemy = new HashSet<GameObject>();
    public Action<int> _OnSpawnEvent;

    public GameObject Spawn(WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case WorldObject.None:
                break;
            case WorldObject.Enemy:
                enemy.Add(go);
                _OnSpawnEvent.Invoke(1);
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
                        if (_OnSpawnEvent != null)
                            _OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
        }
        Managers.Resource.Destroy(go, time);
    }

    public void Clear()
    {
        Spawner spanwer = player.GetComponentInChildren<Spawner>();
        spanwer.gameObject.SetActive(false);

        enemy.Clear();
        Despawn(player);
    }


}
