using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerEnterHandler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Button button;
    //private BaseMenu baseMenu;
    private int index = default;

    RecipeUiController _recipeUiController;

    public int Index { get => index; set => index = value; }


    //public void Init(BaseMenu baseMenu, int i)
    //{
    //    this.baseMenu = baseMenu;
    //    Index = i;
    //}

    private void Start()
    {
        button = this.GetComponent<Button>();
        _recipeUiController = transform.parent.GetComponent<RecipeUiController>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _recipeUiController.ShowRecipe();

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _recipeUiController.HideRecipe();

    }

    //public bool IsStandaloneOrConsole()
    //{
    //    if (GameController.Instance && GameController.Instance.MyCursor && GameController.Instance.MyCursor.IsStandaloneOrConsole)
    //        return true;

    //    return false;

    //}

    //public bool IsCursorEnabled()
    //{
    //    if (GameController.Instance && GameController.Instance.MyCursor && GameController.Instance.MyCursor.CursorTrailRenderer)
    //    {
    //        CursorTrailRenderer _trailRenderer = GameController.Instance.MyCursor.CursorTrailRenderer;

    //        if (_trailRenderer.GetComponent<Renderer>().enabled)
    //            return true;
    //    }

    //    return false;
    //}

    //public bool IsCursorEnabled()
    //{
    //    if(GameController.Instance && GameController.instance.MyCursor)
    //    {
    //        if (GameController.instance.MyCursor.CursorTrailRenderer && GameController.instance.MyCursor.CursorTrailRenderer.Enabled)
    //            return true;
    //    }

    //    return false; 
    //}

}
