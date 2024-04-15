using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Entity
{
    [SerializeField]
    private Animator anim;
    public Animator Anim
    {
        get
        {
            if (anim == null)
            {
                anim = GetComponent<Animator>();
            }
            return anim;
        }
    }

    private Rigidbody rb;
    public Rigidbody RigBody
    {
        get
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }
            return rb;
        }
    }

    Vector3 movepos;

    Vector3 startPos;
    Vector3 endPos;


    float hAxis;
    float vAxis;

    /// <summary>
    /// 플레이어 이동.
    /// </summary>
    private void Move()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        movepos = new Vector3(hAxis, 0, vAxis).normalized;
        if (!CheckHitWall(movepos))
        {
            movepos = Vector3.zero;
        }
        
        transform.position += movepos * stat.MoveSpeed * Time.deltaTime;

        if(movepos != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movepos), 20f * Time.deltaTime);
        }
        
        anim.SetBool("IsWalk", movepos != Vector3.zero);
    }
    /// <summary>
    /// 벽이 있는지 확인.
    /// </summary>
    private bool CheckHitWall(Vector3 _move)
    {
        if(Physics.Raycast(transform.position, _move, out RaycastHit hit, 0.7f))
        {
            if(hit.collider.CompareTag("Wall"))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 플레이어 데미지 및 죽음 구현
    /// </summary>
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "EnemyBullet")
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
        if(stat.HP <= 0)
        {
            transform.GetComponent<CapsuleCollider>().enabled = false;
            GameManager.Instance.GameEndTimeStop(0);
            stat.HP = 0;
            Managers.UI.ShowPopupUI<UI_Fail>();
        }
    }

    public int GetStatDamage() => stat.Damage;
}
