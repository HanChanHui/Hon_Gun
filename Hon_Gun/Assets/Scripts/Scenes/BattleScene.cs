using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : BaseScene
{
    public override ScenesType scenesType { get { return ScenesType.BattleScene; } }
    protected override void Init()
    {
        base.Init();

        GameManager.Instance.Init();
        Managers.UI.ShowSceneUI<UI_Battle>();
    }


    public override void Clear()
    {
        Managers.UI.UIClear();
    }
}
