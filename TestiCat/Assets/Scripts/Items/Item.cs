using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnChange()
    {
        SaveItem();
    }

    public void SaveItem()
    {
        ItemManager.Instance.AddOrUpdateItem(name, gameObject);
    }
}
