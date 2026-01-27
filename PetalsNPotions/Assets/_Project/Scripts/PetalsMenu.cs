using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PetalsMenu : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private List<FlowerData> flowersData = new List<FlowerData>();
    
    [SerializeField] private List<PetalUiController> petalUiControllers = new List<PetalUiController>();
    [SerializeField] private Sprite _emptyFlask;
    [SerializeField] private Sprite _filledFlask;
    [SerializeField] private SpriteRenderer _Flask;

    private List<PetalUiController> appliedPetals = new List<PetalUiController>();
    private bool shouldShowMenu = false;
    private bool verifyFormulaStatus = false;
    private int appliedCountRequired = 3;

    [SerializeField] private FormulaData activeFormulaData;

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

        if (appliedPetals.Count >= appliedCountRequired && !verifyFormulaStatus)
        {
            verifyFormulaStatus = true;
            bool isFormulaValid = GameController.Instance.IsFormulaValid(activeFormulaData.formulaName, appliedPetals);

            if (!isFormulaValid)
            {
                EmptyAppliedPetals();
                verifyFormulaStatus = false;
            }
            else
            {
                //change image for the potion from empty to half filled;
                _Flask.sprite = _filledFlask;
                GameController.Instance.FlaskTriggerController.IsPotionPrepared = true;

                StartCoroutine(AnimateFlask());
            }
        }
    }

    private IEnumerator AnimateFlask()
    {
        float initialYPosition = _Flask.transform.position.y;
        float targetPosition = _Flask.transform.position.y + 0.5f;

        float posY = initialYPosition;

        while (posY < (targetPosition - 0.05f))
        {
            Debug.LogFormat("PosY: {0} , Target Position: {1}", posY, targetPosition);
            posY = Mathf.MoveTowards(posY, targetPosition, Time.fixedDeltaTime);
            _Flask.transform.position = new Vector3(_Flask.transform.position.x, posY, _Flask.transform.position.z);
            yield return null;
        }

        Debug.Log("Flask Animation upwards Completed");

        yield return new WaitForSeconds(0.4f);

        while (posY > initialYPosition)
        {
            posY = Mathf.Lerp(_Flask.transform.position.y, initialYPosition, Time.fixedDeltaTime);
            _Flask.transform.position = new Vector3(_Flask.transform.position.x, posY, _Flask.transform.position.z);
            yield return null;
        }


        yield break;
    }

    public void AddToAppliedFlowers(PetalUiController flower)
    {
        appliedPetals.Add(flower);
    }

    public void EmptyAppliedPetals()
    {
        for (int i = 0; i < appliedPetals.Count; i++)
        {
            for (int j = 0; j < petalUiControllers.Count; j++)
            {
                if (petalUiControllers[j].name == appliedPetals[i].name)
                {
                    petalUiControllers[j].UpdateCount(1);
                    break;
                }
            }
        }

        appliedPetals.Clear();
    }
}
