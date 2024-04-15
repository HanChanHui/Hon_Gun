using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    CameraMode mode = CameraMode.TopView;


    [SerializeField]
     Vector3 delta;
    [SerializeField]
    GameObject player;

    private void Start()
    {
        player = GameManager.Instance.GetPlayer();
    }

    private void LateUpdate()
    {
        if(mode == CameraMode.TopView && player != null)
        {
            transform.position = player.transform.position + delta;
            transform.LookAt(player.transform);
        }
    }
}
