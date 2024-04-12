using System;
using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_EquipmentSelect : UI_Scene
{
    
    [SerializeField]
    private Image[] WeaponImg;
    [SerializeField]
    private Image SelectImg;


    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(EquipImages));
        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
        Bind<Button>(typeof(Buttons));

        SetWeaponImg();
    }

    private void SetWeaponImg()
    {
        int count = System.Enum.GetValues(typeof(EquipImages)).Length;
        for (int i = 0; i < count; i++)
        {
            //WeaponImg[i] = GetImage((int)Images.WeaponImg_1);
            WeaponImg[i].sprite = Resources.Load<Sprite>(Consts.Path.EquipIcon + i);
            GetButton(i).gameObject.AddUIEvent(SelectWeaponImg);
        }  
    }

    void SelectWeaponImg(PointerEventData data)
    {
       
    }

}
