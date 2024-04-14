using Consts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public partial class Enemy : Entity
{
    private NavMeshAgent agent;

    private GameObject target;

    private Vector3 endPos;

    private void PlayerTrace()
    {
        player = GameManager.Instance.GetPlayer();
        if(player == null )
        {
            return;
        }

        float distance = (player.transform.position - transform.position).magnitude;
        if(distance >= attackRange)
        {
            state = States.Move;
        }
        else
        {
            state = States.Attack;
        }
    }

    private void TypeMovement(EnemysType _enemyType)
    {
        switch( _enemyType )
        {
            case EnemysType.Enemy_1:
                StandAttack();
                break;
            case EnemysType.Enemy_2:
                AttackWhileMove();
                break;
            case EnemysType.Enemy_3:
                AttackWhileMove();
                break;
        }
    }

    private void StandAttack()
    {
        RangeAttack();

        if(player != null)
        {
            Vector3 dir = player.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 10 * Time.deltaTime);
        }
        
    }

    private void AttackWhileMove()
    {
        if (player != null)
        {
            RangeAttack();

            endPos = player.transform.position;
            Vector3 dir = endPos - transform.position;
            float distance = dir.magnitude;

            if (distance < attackRange)
            {
                agent.isStopped = true;
                agent.speed = 0;
            }
            else
            {
                agent.isStopped = false;
                agent.speed = stat.MoveSpeed;
                agent.SetDestination(endPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// 플레이어 데미지 및 죽음 구현
    /// </summary>
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "PlayerBullet")
        {
            if (col.collider.TryGetComponent(out Projectile bullet))
            {
                OnDamaged(bullet.GetComponent<Projectile>().Damage);
            }
        }
    }

    /// <summary>
    /// 플레이어 데미지 구현
    /// </summary>
    public override void OnDamaged(int damage, float force = 0)
    {
        stat.HP -= Mathf.Max(damage, 1);
        OnDead();
    }

    /// <summary>
    /// 플레이어 죽음 구현
    /// </summary>
    public override void OnDead()
    {
        if (stat.HP <= 0)
        {
            transform.GetComponent<CapsuleCollider>().enabled = false;
            Destroy(gameObject);
            stat.HP = 0;
            //Managers.UI.ShowPopupUI<>();
        }
    }
}
