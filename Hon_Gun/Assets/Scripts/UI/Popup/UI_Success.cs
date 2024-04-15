using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Success : UI_Popup
{
    public override PopupUIGroup PopupID { get { return PopupUIGroup.UI_Success; } }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.CheckButton).gameObject.AddUIEvent(OnPresstoNext);
        GameManager.Instance.Clear();
    }

    private void OnPresstoNext(PointerEventData data)
    {
        
        GameManager.Instance.GameEndTimeStop(1);
        SceneManagerEX.Instance.LoadScene(ScenesType.EquipmentSelectScene);
    }



}
