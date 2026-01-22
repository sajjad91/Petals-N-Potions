using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PetalUiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;

    private PetalsMenu _petalsMenu;
    int id = 0;
    string name = "";
    FlowerRarity _flowerRarity;
    int count = 0;

    private int previousValue = 0;

    public void Initialize(FlowerData _flowerData , PetalsMenu instance)
    {
        id = _flowerData.id;
        name = _flowerData.flowerName;
        _flowerRarity = _flowerData._flowerRarity;
        count = _flowerData.inventoryTotal;
        _countText.text = count.ToString();
        _petalsMenu = instance;
    }

    public void InitializePreviouseValue()
    {
        previousValue = count;
    }

    public void UpdateCount(int value)
    {
        count += value;
        _countText.text = count.ToString();
    }

    public void OnClickCallback()
    {
        if(count > 0)
        {
            count--;
            _countText.text = count.ToString();
            _petalsMenu.AddToAppliedFlowers(name);
        }
    }

    public int GetCurrentCount()
    {
        return count;
    }


}
