using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));


        slider = GetSlider((int)HpSlider.UI_HpBar).GetComponent<Slider>();
        GetImage((int)SelectWeaponEquip.WeaponEquip).GetComponent<Image>().sprite = Resources.Load<Sprite>(Path.EquipIcon + Managers.Data.GetWeaponData());
        Get<Text>((int)Texts.KillCountText).text = "" + 0;
        GetButton((int)Buttons.SpawnTimeUp).gameObject.AddUIEvent(SpawnTimeUpEvent);
    }

    private void Start()
    {
        stat = GameManager.Instance.GetPlayer().GetOrAddComponent<Player>().GetComponent<Stat>();
        Debug.Log(stat);
    }


    public void BattleUIUpdate(InGameData _data)
    {
        if (slider != null && stat != null)
        {
            HpState(slider);
        }
        SetKillCount(_data.killCount);
    }

    private void SetKillCount(int _kill)
    {
        Get<Text>((int)Texts.KillCountText).text = "" + _kill;
    }

    private void SpawnTimeUpEvent(PointerEventData data)
    {
        GameManager.Instance.InGameData.coolTime = 3;
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
