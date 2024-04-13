using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{

    private GameObject player;

    public GameObject GetPlayer() => player;


    private IEnumerator InGameStart()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        yield return null;
    }


    public void Init()
    {
        StartCoroutine(InGameStart());
    }
}
