using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private List<GameObject> _poolObjects;
    private int _poolAmount;
    private bool _willGrow;

    [SerializeField]
    private ObjectPoolerScriptableObject _poolerScriptableObject;

    private List<GameObject> poolList;

    private void Awake()
    {
        _poolObjects = _poolerScriptableObject.poolObjectVariants;
        _poolAmount = _poolerScriptableObject.poolAmount;
        _willGrow = _poolerScriptableObject.willGrow;

        poolList = new List<GameObject>();
        int index = 0;
        for (int i = 0; i < _poolAmount; i++)
        {
            if(index >= _poolObjects.Count)
            {
                index = 0;
            }
            GameObject obj = Instantiate(_poolObjects[index]);
            index++;

            obj.transform.SetParent(transform, true);
            obj.SetActive(false);
            poolList.Add(obj);
        }
    }

    public void ActivatePoolObjects(int amountToActivate)
    {
        for (int i = 0; i < amountToActivate; i++)
        {
            poolList[i].SetActive(true);
        }
    }

    public GameObject GetPooledObject()
    {
        var activeObjects = poolList.Where(obj => obj.activeInHierarchy == false);
        if(activeObjects.Count() > 0)
        {
            int index = Random.Range(0, activeObjects.Count());
            return activeObjects.ElementAt(index);
        }

        if (_willGrow)
        {
            int index = Random.Range(0, _poolObjects.Count - 1);
            GameObject obj = Instantiate(_poolObjects.ElementAt(index));
            poolList.Add(obj);
            obj.transform.SetParent(transform, true);
            return obj;
        }

        return null;
    }
}
