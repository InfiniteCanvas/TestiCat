using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    public static ItemManager Instance
    {
        get
        {
            Debug.Assert(_Instance != null, "There is no ItemManager object!");
            return _Instance;
        }
    }
    private static ItemManager _Instance;

    private Dictionary<int, GameObject> _items = new Dictionary<int, GameObject>();

    private void Awake()
    {
        _Instance = this;
        DontDestroyOnLoad(this);
    }

    public void AddOrUpdateItem(string name, GameObject item)
    {
        int nameHash = name.GetHashCode();       
        if (_items.ContainsKey(nameHash))
        {
            Debug.Log(string.Format("The item [{0}] already exists. Overwriting..", name));
            _items[nameHash] = item;
        }
        else
        {
            Debug.Log(string.Format("The item [{0}] has been added.", name));
            _items.Add(nameHash, item);
        }
    }

    public GameObject GetItem(string name)
    {
        int nameHash = name.GetHashCode();
        if (_items.ContainsKey(nameHash))
            return _items[nameHash];

        Debug.Log(string.Format("The item [{0}] does not exist.", name));
        return null;
    }

    public void ResetItems()
    {
        _items = new Dictionary<int, GameObject>();
    }
}
