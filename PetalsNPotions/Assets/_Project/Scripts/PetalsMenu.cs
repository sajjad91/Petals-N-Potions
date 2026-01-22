using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PetalsMenu : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private List<FlowerData> flowersData = new List<FlowerData>();
    private List<PetalUiController> petalUiControllers = new List<PetalUiController>();
    private List<string> appliedPetals = new List<string>();
    private bool shouldShowMenu = false;

    private bool verifyFormulaStatus = false;

    private int appliedCountRequired = 3;

    private void Start()
    {
        flowersData = GameController.Instance.GetFlowerDatas();

        for (int i = 0; i < petalUiControllers.Count; i++)
        {
            petalUiControllers[i].Initialize(flowersData[i], this);
        }

        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

    public void ShowMenu()
    {
        for (int i = 0; i < petalUiControllers.Count; i++)
        {
            petalUiControllers[i].Initialize(flowersData[i], this);
        }

        gameObject.SetActive(true);
        shouldShowMenu = true;
    }

    public void HideMenu()
    {
        shouldShowMenu = false;

        for(int i = 0; i < petalUiControllers.Count; i++)
        {
            GameController.Instance.UpdateFlowerInventory(petalUiControllers[i].name, petalUiControllers[i].GetCurrentCount());
        }
    }

    // Update is called once per frame
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

        if(appliedPetals.Count >= appliedCountRequired && !verifyFormulaStatus)
        {
            verifyFormulaStatus = true;


        }
        
    }

    public void AddToAppliedFlowers(string flowerName)
    {
        appliedPetals.Add(flowerName);
    }

    public void EmptyAppliedPetals()
    {
        for (int i = 0; i < appliedPetals.Count; i++)
        {
            for (int j = 0; j < petalUiControllers.Count; j++)
            {
                if (petalUiControllers[j].name == appliedPetals[i])
                {
                    petalUiControllers[j].UpdateCount(1);
                    break;
                }
            }
        }
    }
}
