using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;



public class UI_Title : UI_Scene
{

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
        Bind<Button>(typeof(Buttons));

        GetText((int)TextMeshProUGUIs.FrontImage).gameObject.SetActive(true);
        GetImage((int)Images.BackgroundImg).gameObject.SetActive(true);
        GetButton((int)Buttons.StartBtn).gameObject.SetActive(true);
        GetButton((int)Buttons.StartBtn).gameObject.AddUIEvent(SetPresstoStart);
    }


    void SetPresstoStart(PointerEventData data)
    {
        SceneManagerEX.Instance.LoadScene(ScenesType.EquipmentSelectScene);
    }
}

