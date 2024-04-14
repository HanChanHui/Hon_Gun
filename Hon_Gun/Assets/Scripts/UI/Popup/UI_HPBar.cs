using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Player player;

    public override void Init()
    {
        Bind<GameObject>(typeof(PlayerHPBar));
        player = GetComponent<Player>();


        player.OnPlayerHPCheck -= HPStateSlider;
        player.OnPlayerHPCheck += HPStateSlider;


        slider = GetObject((int)PlayerHPBar.HpBar).GetComponent<Slider>();
        player.HpState(slider);

    }


    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + new Vector3(0, 1.3f, 0);
    }

    private void HPStateSlider()
    {
        player.HpState(slider);
    }

    private void OnDisable()
    {
        player.OnPlayerHPCheck -= HPStateSlider;
    }

}
