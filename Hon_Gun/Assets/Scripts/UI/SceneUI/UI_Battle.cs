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
    private Player player;

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
        player = GameManager.Instance.GetPlayer().GetOrAddComponent<Player>();

        player.OnPlayerHPCheck -= PlayerHPBarUI;
        player.OnPlayerHPCheck += PlayerHPBarUI;
        PlayerHPBarUI();
    }


    private void PlayerHPBarUI()
    {
        player.HpState(slider);
    }



}
