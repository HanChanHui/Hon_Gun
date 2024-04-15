using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Consts
{
    public enum WorldObject
    {
        None,
        Player,
        Enemy,
    }


    public enum EnemysType
    {
        None,
        Enemy_1,
        Enemy_2,
        Enemy_3,
    }

    public enum States
    {
        None,
        Move,
        Hit,
        Die,
    }


    public enum ScenesType
    {
        None,
        TitleScene,
        EquipmentSelectScene,
        BattleScene,
    }

    public enum CameraMode
    {
        Node,
        TopView,

    }

    public enum PopupUIGroup
    {
        None,
        UI_Success,
        UI_Fail,

    }

    public enum TitleImages
    {
        BackgroundImg,
    }


    public enum Buttons
    {
        StartBtn,
        BattleStartBtn,
        CheckButton,
        SpawnTimeUp,
    }

    public enum Texts
    {
        FrontText,
        KillCountText,
    }

    public enum PlayerHPBar
    {
        HpBar,
    }
    public enum HpSlider
    {
        UI_HpBar,
    }

    public enum EquipButtons
    {
        WeaponBtn_1, WeaponBtn_2, WeaponBtn_3, WeaponBtn_4, WeaponBtn_5, WeaponBtn_6, WeaponBtn_7, WeaponBtn_8,
    }

    public enum EquipImages
    {
        WeaponImg_1, WeaponImg_2, WeaponImg_3, WeaponImg_4, WeaponImg_5, WeaponImg_6, WeaponImg_7, WeaponImg_8,
    }

    public enum SelectGroups
    {
        SelectGroup,
    }

    public enum SelectWeaponEquip
    {
        WeaponEquip,
    }
}
