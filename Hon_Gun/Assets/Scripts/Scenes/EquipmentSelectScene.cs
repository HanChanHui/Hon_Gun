using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSelectScene : BaseScene
{
    public override ScenesType scenesType { get { return ScenesType.EquipmentSelectScene; } }
    protected override void Init()
    {
        base.Init();

        Managers.UI.ShowSceneUI<UI_EquipmentSelect>();
    }


    public override void Clear()
    {
        Managers.UI.UIClear();
    }
}
