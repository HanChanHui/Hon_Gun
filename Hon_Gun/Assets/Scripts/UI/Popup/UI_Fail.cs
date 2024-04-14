using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Fail : UI_Popup
{
    public override PopupUIGroup PopupID { get { return PopupUIGroup.UI_Fail; } }
    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.CheckButton).gameObject.AddUIEvent(OnPresstoNext);
    }

    private void OnPresstoNext(PointerEventData data)
    {
        SceneManagerEX.Instance.LoadScene(ScenesType.EquipmentSelectScene);
    }
}
