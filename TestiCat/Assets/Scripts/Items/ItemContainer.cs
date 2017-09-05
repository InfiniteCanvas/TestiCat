using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private GameObject[] _items;

    private void Start()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            GameObject savedItem = ItemManager.Instance.GetItem(_items[i].name);
            if (savedItem != null)
                ReplaceItem(i, savedItem);
        }
    }

    private void ReplaceItem(int itemIndex, GameObject item)
    {
        Destroy(_items[itemIndex]);
        _items[itemIndex] = Instantiate(item);
    }
}
