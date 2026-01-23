using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    [SerializeField] private PetalsMenu _petalsMenu;
    [SerializeField] private RecipesMenu _recipeMenu;
    [SerializeField] private Gameplayscreen _gameplayScreen;
    [SerializeField] private DialogueBoxManager _npcDialogBoxManager;

    private bool shouldShowCollectedAnimation = false;
    private bool shouldShowNpcDialogBox = false;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        
    }

    public void ShowPetalsMenu()
    {
        _petalsMenu.ShowMenu();
        _gameplayScreen.HideRecipeHud();
    }

    public void ShowRecipeHud()
    {
        _gameplayScreen.ShowRecipeHud();
    }

    public void HidePetalsMenu()
    {
        _petalsMenu.HideMenu();
    }

    public void ShowRecipesMenu()
    {
        _recipeMenu.ShowMenu();
    }

    public void HideRecipesMenu()
    {
        _recipeMenu.HideMenu();
    }

    public void UpdateRecipeCollectedCount()
    {
        _gameplayScreen.UpdateRecipeCollectedCount();
    }

    public void ShowRecipeCollectedAnimation()
    {
        _gameplayScreen.ShowRecipeCollectedAnimation();
    }

    public void UnlockRecipe(FormulaType _formulaType)
    {
        _recipeMenu.UnlockRecipe(_formulaType);
    }

    public void ShowNpcDialogue(int index, bool isRewardingDialogue = default)
    {
        _npcDialogBoxManager.ShowDialogue(index, isRewardingDialogue);
    }

    public void HideNpcDialogue()
    {
        _npcDialogBoxManager.HideDialogue();
    }

    public void UpdateCoinsCount(int value)
    {
        _gameplayScreen.UpdateCoins(value);
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