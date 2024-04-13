using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : Entity
{
    [SerializeField]
    private Slider hpBar;


    public void HpState()
    {
        if(stat.HP > stat.MaxHP)
        {
            stat.HP = stat.MaxHP;
        }
        float ratio = stat.HP / stat.MaxHP;
        SetHpRatio(ratio);
    }

    private void SetHpRatio(float _ratio)
    {
        if(_ratio < 0)
        {
            _ratio = 0;
        }
        if(_ratio > 1)
        {
            _ratio = 1;
        }

        hpBar.value = _ratio;
    }
}
