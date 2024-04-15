using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;




public class UI_Title : UI_Scene
{

    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(TitleImages));
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GetText((int)Texts.FrontText).gameObject.SetActive(true);
        GetImage((int)TitleImages.BackgroundImg).gameObject.SetActive(true);
        GetButton((int)Buttons.StartBtn).gameObject.SetActive(true);
        GetButton((int)Buttons.StartBtn).gameObject.AddUIEvent(SetPresstoStart);
    }


    void SetPresstoStart(PointerEventData data)
    {
        SceneManagerEX.Instance.LoadScene(ScenesType.EquipmentSelectScene);
    }
}


