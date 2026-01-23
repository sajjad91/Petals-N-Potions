using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeSecretController : MonoBehaviour
{
    public FormulaType _formulaType;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("player entered recipe secret trigger");

        if (other.transform.parent.name == "Herbalist")
        {
            if(MenuController.Instance)
            {
                MenuController.Instance.ShowRecipeCollectedAnimation();
                MenuController.Instance.UpdateRecipeCollectedCount();
                MenuController.Instance.UnlockRecipe(_formulaType);
                Destroy(gameObject);
            }
        }

    }
}
