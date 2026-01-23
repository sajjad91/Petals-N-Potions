using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeUiController : MonoBehaviour
{
    private bool isLocked = true;

    [SerializeField] private GameObject lockIcon;
    [SerializeField] private GameObject recipeDetail;
    [SerializeField] private GameObject recipeTitle;

    public FormulaType _formulaType;

    // Start is called before the first frame update
    void Start()
    {
        lockIcon.SetActive(false);
        recipeDetail.SetActive(false);
        recipeTitle.SetActive(true);
    }

    public void ShowRecipe()
    {
        if(!isLocked)
        {
            recipeDetail.SetActive(true);
            recipeTitle.SetActive(false);
            lockIcon.SetActive(false);
            return;
        }

        lockIcon.SetActive(true);
        recipeDetail.SetActive(false);
        recipeTitle.SetActive(false);
    }

    public void HideRecipe()
    {
        recipeDetail.SetActive(false);
        lockIcon.SetActive(false);
        recipeTitle.SetActive(true);
    }

    public void Unlock()
    {
        isLocked = false;
    }
}
