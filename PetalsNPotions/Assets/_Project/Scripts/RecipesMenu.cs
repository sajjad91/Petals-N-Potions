using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesMenu : MonoBehaviour
{
    private bool shouldShowMenu = false;
    private CanvasGroup _canvasGroup;

    [SerializeField] private List<RecipeUiController> recipeUiControllers = new List<RecipeUiController>();


    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

    public void ShowMenu()
    {
        gameObject.SetActive(true);
        shouldShowMenu = true;
    }

    public void HideMenu()
    {
        shouldShowMenu = false;
    }

    public void UnlockRecipe(FormulaType formulaType)
    {
        for(int i = 0; i < recipeUiControllers.Count; i++)
        {
            if (recipeUiControllers[i]._formulaType == formulaType)
            {
                recipeUiControllers[i].Unlock();
            }
        }
    }

    void Update()
    {
        if (shouldShowMenu)
        {
            float lerpAlpha = Mathf.Lerp(_canvasGroup.alpha, 1f, Time.deltaTime * 5f);
            _canvasGroup.alpha = lerpAlpha;
        }
        else
        {
            float lerpAlpha = Mathf.Lerp(_canvasGroup.alpha, 0f, Time.deltaTime * 5f);
            _canvasGroup.alpha = lerpAlpha;
            if (_canvasGroup.alpha <= 0.01f)
            {
                gameObject.SetActive(false);
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideMenu();
            GameplayManager.Instance.DisablePlayerControls = false;
        }
    }
}
