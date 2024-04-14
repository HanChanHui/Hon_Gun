using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : Entity
{
    /// <summary>
    /// 플레이어가 데미지를 입을 시 발생
    /// </summary>
    public Action OnPlayerHPCheck;

    [SerializeField]
    private Slider hpBar;


    private void HPEvent()
    {
        hpBar = GetComponentInChildren<Slider>();

        OnPlayerHPCheck -= HpState;
        OnPlayerHPCheck += HpState;

        HpState(hpBar);
    }


    public void HpState()
    {
        HpState(hpBar);
    }

    /// <summary>
    /// 플레이어의 HP 상태 확인
    /// </summary>
    public void HpState(Slider _hpBar)
    {
        if(stat.HP > stat.MaxHP)
        {
            stat.HP = stat.MaxHP;
        }
        int ratio = stat.HP;
        _hpBar.value = SetHpRatio(ratio);
    }

    /// <summary>
    /// 플레이어의 HP 비율
    /// </summary>
    private int SetHpRatio(int _ratio)
    {
        if(_ratio < 0)
        {
            _ratio = 0;
        }
        if(_ratio > 100)
        {
            _ratio = 100;
        }

       return _ratio;
    }

    private void OnDisable()
    {
        OnPlayerHPCheck -= HpState;
    }
}
