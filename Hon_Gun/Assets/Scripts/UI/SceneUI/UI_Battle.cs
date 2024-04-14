using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UI_Battle : UI_Scene
{

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Stat stat;

    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(SelectWeaponEquip));
        Bind<Slider>(typeof(HpSlider));


        slider = GetSlider((int)HpSlider.UI_HpBar).GetComponent<Slider>();
        GetImage((int)SelectWeaponEquip.WeaponEquip).GetComponent<Image>().sprite = Resources.Load<Sprite>(Path.EquipIcon + Managers.Data.GetWeaponData());
    }

    private void Start()
    {
        stat = GameManager.Instance.GetPlayer().GetOrAddComponent<Player>().GetComponent<Stat>();

       
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
