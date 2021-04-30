using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Pool<T> where T : Component 
{
    public readonly Type TypeOfObjectInPool;
    
    private readonly GameObject _prefab;
    private readonly int _startSizeOfPool;

    private int count = 0;
    
    private IEnumerable<T> pool;

    public Pool(IronBellyTestSetting ironBellyTestSetting)
    {
        TypeOfObjectInPool = typeof(T);
        _prefab = ironBellyTestSetting.prefabNeighbor.gameObject;
        _startSizeOfPool = ironBellyTestSetting.startPoolSize;
        SetObjectsInPool();
    }
    
    internal Pool(GameObject prefab, int sizeOfPool)
    {
        TypeOfObjectInPool = typeof(T);
        _prefab = prefab;
        _startSizeOfPool = sizeOfPool;
        SetObjectsInPool();
    }
    
    void SetObjectsInPool()
    {
        List<T> tempList = new List<T>();
        for (int i = 0; i < _startSizeOfPool; i++)
        {
            count++;
            T newObject = Object.Instantiate(_prefab).GetComponent<T>();
            newObject.name = count.ToString();
            tempList.Add(newObject);
        }

        pool = tempList;
    }

    public T Get()
    {
        List<T> list = pool as List<T>;

        if (list.Count != 0)
        {
            T objectToReturn = list[0];
            list.RemoveAt(0);
            return objectToReturn;
        }

        return CreateEmergencyObject();
    }

    public List<T> Get(int amount)
    {
        List<T> tempPool = pool as List<T>;
        List<T> listToReturn = new List<T>();
        
        if (amount < tempPool.Count)
        {
            for (int i = 0; i < amount; i++)
            {
                ExtractFromPool();
            }
        }
        else
        {
            HandleRequiredAmountGreaterThanAvailableInPool();
        }
        return listToReturn;

        void ExtractFromPool()
        {
            T objectInPool = tempPool[0];
            listToReturn.Add(objectInPool);
            tempPool.RemoveAt(0);
        }

        void HandleRequiredAmountGreaterThanAvailableInPool()
        {
            for (int i = 0; i < amount; i++)
            {
                if (tempPool.Count != 0)
                {
                    ExtractFromPool();
                }
                else
                {
                    listToReturn.Add(CreateEmergencyObject());
                }
            }
        }
    }

    private T CreateEmergencyObject()
    {
        Debug.Log("The pool is empty, an object has been created. Consider expanding the initial pool size ");
        count++;
        T createObject = Object.Instantiate(_prefab).GetComponent<T>();
        createObject.name = count.ToString();
        return createObject;
    }
    
    public int GetPoolSize()
    {
        List<T> list = pool as List<T>;
        return list.Count;
    }

    public List<T> GetAllObjectsInPool()
    {
        return pool as List<T>;
    }

    public void Add(T objectToAdd)
    {
        List<T> tempPool = pool as List<T>;
        tempPool.Add(objectToAdd);
        pool = tempPool;
    }
    
    public void Add(List<T> addList)
    {
        List<T> tempPool = pool as List<T>;
        
        int amountToAdd = addList.Count;
        for (int i = 0; i < amountToAdd; i++)
        {
            tempPool.Add(addList[i]);
        }
        
        pool = tempPool;
    }
}