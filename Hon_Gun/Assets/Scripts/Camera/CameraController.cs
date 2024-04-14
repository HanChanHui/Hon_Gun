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



    private void LateUpdate()
    {
        if(mode == CameraMode.TopView && player != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(player.transform.position, delta, out hit, delta.magnitude, LayerMask.GetMask("Wall")))
            {
                float dist = (hit.point - player.transform.position).magnitude * 0.8f;
                transform.position = player.transform.position + delta.normalized * dist;
            }
            else
            {
                transform.position = player.transform.position + delta;
                transform.LookAt(player.transform);
            }
        }
    }
}
