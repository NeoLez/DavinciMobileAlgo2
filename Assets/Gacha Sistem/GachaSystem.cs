using UnityEngine;

public sealed class GachaSystem : MonoBehaviour
{
    [SerializeField]
    private GachaPool[] _myPool;

    private float _totalChance;

    [SerializeField]
    private bool _pitySystemAvailable;
    [SerializeField]
    private int _pitySystemTotalNumber = 20;
    private float _pullCount;

    private void Start()
    {
        CalculateChance();
        Debug.Log(_totalChance);
    }

    private void CalculateChance()
    {
        for (int i = 0; i < _myPool.Length; i++)
        {
            _totalChance += _myPool[i].WeightPull;

            if (_pitySystemAvailable)
            {
                ItemRarity rarity = _myPool[i].Rarity;

                for (int j = 0; j < _myPool[i].Items.Length; j++)
                {
                    _myPool[i].Items[j].Rarity = rarity;
                }
            }
        }
    }

    // Desde el boton x1 y x10.
    public void Summon(int pullNumber)
    {
        for (int i = 0; i < pullNumber; i++)
        {
            ItemSO item;
            if (_pitySystemAvailable)
            {
                item = GetItemByPitySystem();
            }
            else
            {
                item = GetItem();
            }
            Debug.Log($"El gacha system te dio {item.ItemName} de tipo {item.Rarity}");
        }
    }

    private ItemSO GetItem()
    {
        GachaPool temPull = null;

        float randomRarity = Random.Range(0, _totalChance);

        for (int i = 0; i < _myPool.Length; i++)
        {
            randomRarity -= _myPool[i].WeightPull;
            if (randomRarity <= 0)
            {
                temPull = _myPool[i];
                break;
            }
        }

        int randomItem = Random.Range(0, temPull.Items.Length);
        return temPull.Items[randomItem];
    }

    private ItemSO GetItemByPitySystem()
    {
        GachaPool tempPull = null;
        _pullCount++;

        if (_pullCount >= _pitySystemTotalNumber)
        {
            tempPull = _myPool[0];
            Debug.Log("Conseguimos algo picante");
        }
        else
        {
            float randomRarity = Random.Range(0, _totalChance);
            for (int i = 0; i < _myPool.Length; i++)
            {
                randomRarity -= _myPool[i].WeightPull;
                if (randomRarity <= 0f)
                {
                    tempPull = _myPool[i];
                    break;
                }
            }
        }

        if (tempPull.Rarity == ItemRarity.Legendary)
        {
            _pullCount = 0;
        }

        int randomItem = Random.Range(0, tempPull.Items.Length);
        return tempPull.Items[randomItem];
    }
}