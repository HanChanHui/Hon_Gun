using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BattleScene : BaseScene
{
    public override ScenesType scenesType { get { return ScenesType.BattleScene; } }

    [SerializeField]
    private InGameData gameData;
    [SerializeField]
    private UI_Battle uiBattle;

    protected override void Init()
    {
        base.Init();

        GameObject player = GameManager.Instance.Spawn(WorldObject.Player, Path.PlayerPrefab);
        gameData = GameManager.Instance.InGameData;
        uiBattle = Managers.UI.ShowSceneUI<UI_Battle>();
    }

    private void Update()
    {
        if(!gameData.isLive)
        {
            return;
        }

        uiBattle.BattleUIUpdate(gameData);

        if (gameData.killCount >= gameData.clearEnemyCount)
        {
            gameData.isLive = false;
            GameManager.Instance.GameEndTimeStop(0);
            Managers.UI.ShowPopupUI<UI_Success>();
        }
    }

    public override void Clear()
    {
        Managers.UI.UIClear();
    }

   

}
