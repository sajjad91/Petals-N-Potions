using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    [SerializeField] private PetalsMenu _petalsMenu;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowPetalsMenu()
    {
        _petalsMenu.ShowMenu();
    }

    public void HidePetalsMenu()
    {
        _petalsMenu.HideMenu();
    }
}



[Serializable]
public class MenuOption
{
    [SerializeField] private string _optionName;
    [SerializeField] private Sprite _optionIcon;
    public string OptionName => _optionName;
    public Sprite OptionIcon => _optionIcon;
}

[Serializable]
public class FlowerData
{
    public FlowerRarity _flowerRarity;
    public bool isFlowerPicked = false;
    public int inventoryTotal = 0;
    public int id = 0;
    public string flowerName = "";
}

public enum FlowerRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}