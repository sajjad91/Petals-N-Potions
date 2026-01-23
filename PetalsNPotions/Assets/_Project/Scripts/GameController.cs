using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private List<FlowerData> flowers = new List<FlowerData>();
    [SerializeField] private List<FormulaData> formulas = new List<FormulaData>();
    [SerializeField] private FlaskTriggerController _flaskTriggerController;


    public FlaskTriggerController FlaskTriggerController { get => _flaskTriggerController; }
    public List<FlowerData> GetFlowerDatas()
    {
        return flowers;
    }

    private void Awake()
    {
        if(Instance == null)
            {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateFlowersDataAndCount(FlowerData _flowerData, int countValue)
    {
        for (int i = 0; i < flowers.Count; i++)
        {
            if (flowers[i].id == _flowerData.id)
            {
                flowers[i].isFlowerPicked = _flowerData.isFlowerPicked;
                flowers[i].inventoryTotal += countValue;
                Debug.LogFormat("Flowers Inventory Value: {0} , Name: {1} " , flowers[i].inventoryTotal, flowers[i].flowerName);
                break;
            }
        }
    }

    public void GetFlowerCount(FlowerData _flowerData)
    {

    }

    public List<FormulaData> GetFormulasData()
    {
        return formulas;
    }

    public FormulaData GetFormulaByType(FormulaType _type)
    {
        for(int i = 0; i < formulas.Count; i++)
        {
            if (formulas[i]._type == _type)
                return formulas[i];
        }
        return null;
    }

    public void UpdateFlowerInventory(string flowerName, int count)
    {
        for(int i = 0; i < flowers.Count; i++)
        {
            if(flowers[i].flowerName == flowerName)
            {
                flowers[i].inventoryTotal = count;
                break;
            }
        }
    }

    public bool IsFormulaValid(string name, List<PetalUiController> appliedFlowersName)
    {
        for (int i = 0; i < formulas.Count; i++)
        {
            if (name == formulas[i].formulaName)
            {

                for (int j = 0; j < formulas[i].requiredFlowerNames.Count; j++)
                {
                    if (!formulas[i].requiredFlowerNames[j].Contains(appliedFlowersName[j].name))
                        return false;
                }

                return true;
            }
        }

        return false;
    }
}

[Serializable]
public class FormulaData
{
    public FormulaType _type;
    public string formulaName;
    public List<string> requiredFlowerNames = new List<string>();
}

public enum FormulaType
{
    Healing,
    Stamina,
    Strength,
    Speed
}