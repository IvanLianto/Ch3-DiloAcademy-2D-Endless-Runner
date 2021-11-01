using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling SharedInstance;

    private Dictionary<string, List<GameObject>> pool = new Dictionary<string, List<GameObject>>();

    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetItemFromPool(GameObject item, Transform parent)
    {
        if (pool.ContainsKey(item.name))
        {
            if (pool[item.name].Count > 0)
            {
                var newItemFromPool = pool[item.name][0];
                pool[item.name].Remove(newItemFromPool);
                newItemFromPool.SetActive(true);
                return newItemFromPool;
            }
        }
        else
        {
            pool.Add(item.name, new List<GameObject>());
        }

        GameObject newItem = Instantiate(item, parent);
        newItem.name = item.name;
        return newItem;
    }

    public void ReturnToPool(GameObject item)
    {
        if (!pool.ContainsKey(item.name))
        {
            Debug.LogWarning("Invalid Item");
        }
        pool[item.name].Add(item);
        item.SetActive(false);
    }

}
