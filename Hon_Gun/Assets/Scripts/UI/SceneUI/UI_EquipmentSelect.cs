using System;
using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class UI_EquipmentSelect : UI_Scene
{
    [SerializeField]
    private Sprite[] weaponImg;
    [SerializeField]
    private Image selectImg;
    [SerializeField]
    private GameObject selectGroup;


    public override void Init()
    {
        base.Init();

       //Bind<Image>(typeof(EquipImages));
        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
        Bind<Image>(typeof(SelectWeaponEquip));
        Bind<GameObject>(typeof(SelectGroups));
        Bind<Button>(typeof(Buttons));

        selectImg = GetImage((int)SelectWeaponEquip.WeaponEquip);
        selectGroup = GetObject((int)SelectGroups.SelectGroup).gameObject;
        GetButton((int)Buttons.BattleStartBtn).gameObject.AddUIEvent(SetPresstoStart);
        
        SetWeaponImg();
    }

    private void SetWeaponImg()
    {
        int count = System.Enum.GetValues(typeof(EquipImages)).Length;
        weaponImg = new Sprite[count];
        for (int i = 0; i < count; i++)
        {
            GameObject btn = Managers.Resource.Instantiate(Path.EquipButton);
            btn.transform.SetParent(selectGroup.transform);
            btn.transform.localScale = Vector3.one;
            btn.transform.localPosition = new Vector3(0f, 0f, 0f);
            

            UI_ButtonData weaponData = btn.GetOrAddComponent<UI_ButtonData>();
            weaponData.SetButtonData(Resources.Load<Sprite>(Path.EquipIcon + i), (EquipImages)i, (EquipButtons)i);
            btn.name = weaponData.GetButtonType().ToString();
            weaponImg[i] = weaponData.GetSprite();

            if(weaponData!= null)
            {
                weaponData.OnButtonClicked += SelectWeaponImg;
            }
        }
    }

    public void SelectWeaponImg(int i)
    {
        selectImg.sprite = weaponImg[i];
        Managers.Data.SetWeaponData(i);
    }

    void SetPresstoStart(PointerEventData data)
    {
        SceneManagerEX.Instance.LoadScene(ScenesType.BattleScene);
    }

}
