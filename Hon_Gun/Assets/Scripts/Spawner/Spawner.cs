using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float coolTime;

    [Header("Raycast Settings")]
    private float rengeOfCheck = 5;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Vector2 positivePosition, negativePosition;
    [SerializeField]
    private GameObject enemy;


    private void Start()
    {
        StartCoroutine(SpawnEnemy(Path.EnemyPrefab));
    }



    private IEnumerator SpawnEnemy(string _path)
    {
        yield return new WaitUntil(() => GameManager.Instance.InGameData.isLive);

        enemy = GameManager.Instance.Spawn(WorldObject.Enemy, _path + RandomEnmeyType());
        enemy.transform.position = RandomPos();
        coolTime = GameManager.Instance.InGameData.coolTime;
        yield return new WaitForSeconds(coolTime);

        StartCoroutine(SpawnEnemy(Path.EnemyPrefab));
    }

    /// <summary>
    /// ���� ���� ����.
    /// </summary>
    private int RandomEnmeyType()
    {
        int length = System.Enum.GetValues(typeof(EnemysType)).Length;
        return Random.Range(1, length); // None�� ������ enum.
    }

    /// <summary>
    /// ���� ���� ���� Pos ����.
    /// �ùٸ� ��ġ�� ������ ������ �ݺ�.
    /// </summary>
    private Vector3 RandomPos()
    {
        Vector3 pos  = new Vector3();
        int count = 10;
        while(count > 0)
        {
            float random_x = Random.Range(positivePosition.x, negativePosition.x);
            float random_z = Random.Range(positivePosition.y, negativePosition.y);
            
            if (FloorCheck(random_x, random_z))
            {
                pos = new Vector3(random_x, 0, random_z);
                break;
            }
            count--;
        }

        return pos;
    }

    /// <summary>
    /// �ٴ��� �ִ���, ������ üũ.
    /// </summary>
    private bool FloorCheck(float _x, float _z)
    {
        RaycastHit hit;
        return Physics.Raycast(new Vector3(_x, 1f, _z), Vector3.down, out hit, rengeOfCheck, layerMask);

    }




}
