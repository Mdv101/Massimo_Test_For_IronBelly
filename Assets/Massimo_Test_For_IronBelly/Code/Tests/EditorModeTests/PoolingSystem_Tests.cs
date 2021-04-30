using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolingSystem_Tests
{
    private PoolFactory poolFactory;
    private Transform prefabTransform;
    [SetUp]
    public void SetUp()
    { 
        GameObject gameObject = new GameObject();
        poolFactory = gameObject.AddComponent<PoolFactory>();
        
        prefabTransform = Resources.Load<Transform>("TestResources/GameObject_OnlyTransform");
    }

    [Test]
    public void WhenCreatePool_ThenReturnCorrectPool()
    {
        Type expectedType = typeof(Transform);
        int defaultSizeOfPool = poolFactory.DefaultPoolSize;
        
        //Act
        Pool<Transform> pool = poolFactory.CreatePool<Transform>(prefabTransform.gameObject);

        //Asserts
        Assert.AreEqual(expectedType, pool.TypeOfObjectInPool,
            $"The type of object in pool, expected {expectedType.Name}, get {pool.TypeOfObjectInPool.Name}");
        
        Assert.AreEqual(defaultSizeOfPool,
            pool.GetPoolSize(),$"The pool dont have the correct size, expected {defaultSizeOfPool}");
        
    }

    [Test]
    public void WhenGetObject_PoolReduceSize()
    {
        Pool<Transform> pool = poolFactory.CreatePool<Transform>(prefabTransform.gameObject);

        //Act
        int expectedSize = pool.GetPoolSize() - 1;
        pool.Get();
        
        
        //Asserts
        Assert.AreEqual(expectedSize,pool.GetPoolSize(),
            $"The pool dont reduce size, expected {expectedSize}, get {pool.GetPoolSize()}");
    }
    
    
    [TestCase(2)]
    [TestCase(4)]
    public void WhenGetObjects_PoolReduceSize(int amount)
    {
        Pool<Transform> pool = poolFactory.CreatePool<Transform>(prefabTransform.gameObject);

        //Act
        int expectedSize = pool.GetPoolSize() - amount;
        List<Transform> listGet = pool.Get(amount);
        
        
        //Asserts
        Assert.AreEqual(expectedSize,pool.GetPoolSize(), 
            $"The pool dont reduce size, expected {expectedSize}, get {listGet.Count}");
        
        Assert.AreEqual(amount,listGet.Count,
            $"The List size does not match the required quantity, expected {amount}, get {pool.GetPoolSize()}");
    }

    [Test]
    public void WhenAddObject_PoolIncreasesSize()
    {
        Pool<Transform> pool = poolFactory.CreatePool<Transform>(prefabTransform.gameObject);
        
        int expectedSize = pool.GetPoolSize() + 1;
        Transform newTransform = Object.Instantiate(prefabTransform);
        pool.Add(newTransform);
        
        Assert.AreEqual(expectedSize,pool.GetPoolSize(),
            $"The pool dont increase size, expected {expectedSize}, get {pool.GetPoolSize()}");
    }
    
    
    [TestCase(2)]
    [TestCase(4)]
    public void WhenAddObjects_PoolIncreasesSize(int amount)
    {
        Pool<Transform> pool = poolFactory.CreatePool<Transform>(prefabTransform.gameObject);
        
        List<Transform> listToAdd = new List<Transform>();
        for (int i = 0; i < amount; i++)
        {
            listToAdd.Add(Object.Instantiate(prefabTransform));
        }
        

        //Act
        int expectedSize = pool.GetPoolSize() + amount;
        pool.Add(listToAdd);
        
        
        //Asserts
        Assert.AreEqual(expectedSize,pool.GetPoolSize());
    }
}
