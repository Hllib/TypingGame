using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private GameObject _poolObject;
    private int _poolAmount;
    private bool _willGrow;

    [SerializeField]
    private ObjectPoolerScriptableObject _poolerScriptableObject;

    private List<GameObject> poolList;

    private void Awake()
    {
        _poolObject = _poolerScriptableObject.poolObjectVariants[0];
        _poolAmount = _poolerScriptableObject.poolAmount;
        _willGrow = _poolerScriptableObject.willGrow;

        poolList = new List<GameObject>();
        for (int i = 0; i < _poolAmount; i++)
        {
            int index = Random.Range(0, _poolerScriptableObject.poolObjectVariants.Length);


            GameObject obj = Instantiate(_poolerScriptableObject.poolObjectVariants[index]);
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
        /*for (int i = 0; i < poolList.Count; i++)
        {
            if (!poolList[i].activeInHierarchy)
            {
                return poolList[i];
            }
        }*/

        var activeObjects = poolList.Where(obj => obj.activeInHierarchy == false);
        if(activeObjects.Count() > 0)
        {
            int index = Random.Range(0, activeObjects.Count());
            return activeObjects.ElementAt(index);
        }

        if (_willGrow)
        {
            GameObject obj = Instantiate(_poolObject);
            poolList.Add(obj);
            obj.transform.SetParent(transform, true);
            return obj;
        }

        return null;
    }
}
