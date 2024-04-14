using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Stat stat;

    public override void Init()
    {
        Bind<GameObject>(typeof(PlayerHPBar));

        slider = GetObject((int)PlayerHPBar.HpBar).GetComponent<Slider>();
    }

    private void Start()
    {
        stat = transform.parent.GetComponent<Stat>();

        Transform parent = transform.parent;
        transform.position = parent.position + new Vector3(0, 1.8f, 0);
    }

    private void Update()
    {
        if(slider != null)
        {
            HpState(slider);
        }
    }

    public void HpState(Slider _hpBar)
    {
        if (stat.HP > stat.MaxHP)
        {
            stat.HP = stat.MaxHP;
        }
        float ratio = stat.HP;
        _hpBar.value = SetHpRatio(ratio);
    }

    /// <summary>
    /// 플레이어의 HP 비율
    /// </summary>
    private float SetHpRatio(float _ratio)
    {
        if (_ratio < 0)
        {
            _ratio = 0;
        }
        if (_ratio > 100)
        {
            _ratio = 100;
        }

        return _ratio;
    }


}
