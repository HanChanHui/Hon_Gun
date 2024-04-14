using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Entity
{

    private Animator _anim;
    public Animator Anim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>();
            }
            return _anim;
        }
    }

    private Rigidbody2D _rb;
    public Rigidbody2D RigBody
    {
        get
        {
            if (_rb == null)
            {
                _rb = GetComponent<Rigidbody2D>();
            }
            return _rb;
        }
    }

    
    private void InputEvent()
    {
        Managers.Input.keyAction -= Movement;
        Managers.Input.keyAction += Movement;
    }

    /// <summary>
    /// 플레이어 움직임 구현
    /// </summary>
    private void Movement()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += Vector3.forward * Time.deltaTime * stat.MoveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += Vector3.back * Time.deltaTime * stat.MoveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += Vector3.left * Time.deltaTime * stat.MoveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += Vector3.right * Time.deltaTime * stat.MoveSpeed;
        }

    }

    /// <summary>
    /// 플레이어 데미지 및 죽음 구현
    /// </summary>
    private void OnCollisionStay(Collision col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            if(col.collider.TryGetComponent(out EnemyStat enemy))
            {
                OnDamaged(enemy.GetComponent<EnemyStat>().Damage);
            }
        }
    }

    /// <summary>
    /// 플레이어 데미지 구현
    /// </summary>
    public override void OnDamaged(int damage, float force = 0)
    {
        stat.HP -= Mathf.Max(damage, 1);
        OnPlayerHPCheck?.Invoke();
        OnDead();
    }

    /// <summary>
    /// 플레이어 죽음 구현
    /// </summary>
    public override void OnDead()
    {
        if(stat.HP <= 0)
        {
            transform.GetComponent<CapsuleCollider2D>().enabled = false;
            stat.HP = 0;
            //Managers.UI.ShowPopupUI<>();
        }
    }
}
