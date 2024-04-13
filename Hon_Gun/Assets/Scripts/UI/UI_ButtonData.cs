using Consts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonData : MonoBehaviour
{
    public event Action<int> OnButtonClicked;

    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private Image Childsprite;
    [SerializeField]
    private EquipImages equipImages;
    [SerializeField]
    private EquipButtons equipButton;

    private void Start()
    {
        var button = GetComponent<Button>();
        if(button != null)
        {
            button.onClick.AddListener(() => OnButtonClicked?.Invoke((int)equipButton));
        }
    }

    public EquipImages GetEquipType()
    {
        return equipImages;
    }
    public EquipButtons GetButtonType()
    {
        return equipButton;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public void SetButtonData(Sprite _sprite, EquipImages _equipImages, EquipButtons equipButton)
    {
        this.sprite = _sprite;
        this.equipImages = _equipImages;
        this.equipButton = equipButton;
        Childsprite.sprite = _sprite;
    }
}
