using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    public override ScenesType scenesType { get { return ScenesType.TitleScene; } }
    protected override void Init()
    {
        base.Init();

        Managers.UI.ShowSceneUI<UI_Title>();
    }


    public override void Clear()
    {
        Managers.UI.UIClear();
    }

}
