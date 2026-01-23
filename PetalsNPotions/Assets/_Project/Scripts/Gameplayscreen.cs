using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gameplayscreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recipeText;
    [SerializeField] private Transform recipeCollectedAnim;
    [SerializeField] private GameObject recipeHud;
    [SerializeField] private GameObject coinsHud;
    [SerializeField] private TextMeshProUGUI coinsText;
    private int collectedCount = 0;
    private int coinsCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _recipeText.text = collectedCount.ToString();
        recipeCollectedAnim.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateRecipeCollectedCount()
    {
        collectedCount++;
        _recipeText.text = collectedCount.ToString();

    }

    public void ShowRecipeCollectedAnimation()
    {
        StartCoroutine(PlayCollectedAnimation());
    }


    private IEnumerator PlayCollectedAnimation()
    {
        while (recipeCollectedAnim.localScale.x < 1.4f)
        {
            recipeCollectedAnim.localScale += (Vector3.one * 4) * Time.deltaTime * 2.5f;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (recipeCollectedAnim.localScale.x > 0)
        {
            recipeCollectedAnim.localScale -= Vector3.one * Time.deltaTime * 2.5f;
            yield return null;
        }

        yield break;
    }

    public void RecipeHudCallback()
    {
        MenuController.Instance.ShowRecipesMenu();
        GameplayManager.Instance.DisablePlayerControls = true;
    }

    public void HideRecipeHud()
    {
        recipeHud.SetActive(false);
        coinsHud.SetActive(false);

    }

    public void ShowRecipeHud()
    {
        recipeHud.SetActive(true);
        coinsHud.SetActive(true);
    }

    public void UpdateCoins(int value)
    {
        coinsCount += value;
        coinsText.text = coinsCount.ToString();
        StartCoroutine(_AnimateCoinsHud());
    }

    private IEnumerator _AnimateCoinsHud()
    {
        while (coinsHud.transform.localScale.x < 1.1f)
        {
            coinsHud.transform.localScale += Vector3.one * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (coinsHud.transform.localScale.x > 1)
        {
            coinsHud.transform.localScale -= Vector3.one * Time.deltaTime;
            yield return null;
        }

        yield break;
    }

}
