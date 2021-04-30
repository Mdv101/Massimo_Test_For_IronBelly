using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[DefaultExecutionOrder(-100)]
public class TestManager : MonoBehaviour
{
    [SerializeField] private IronBellyTestSetting ironBellyTestSetting;

    private List<Neighbor> activeNeighbors;
    private PositionHandler positionHandler;
    private Pool<Neighbor> pool;

    public int ActiveObjectsAmount => activeNeighbors.Count;
    
    private void Awake()
    {
        positionHandler = new PositionHandler(ironBellyTestSetting.maxSearchDistance);
        
        activeNeighbors = new List<Neighbor>();
        Neighbor.OnNeighborEnabled += Initialized;
        
        SetPool();
    }

    private void SetPool()
    {
        pool = new Pool<Neighbor>(ironBellyTestSetting);
        List<Neighbor> poolObjects = pool.GetAllObjectsInPool();
        foreach (var neighbor in poolObjects)
        {
            neighbor.Deactivate();
        }
    }

    private void Update()
    {
        positionHandler.UpdatePositions();
    }

    private void Initialized(Neighbor neighbor)
    {
        neighbor.Initialize(ironBellyTestSetting);
    }

    private void OnDestroy()
    {
        Neighbor.OnNeighborEnabled -= Initialized;
    }

    public void SpawnNeighbors(int amount)
    {
        List<Neighbor> tempList = pool.Get(amount);

        foreach (var neighbor in tempList)
        {
            neighbor.Initialize(ironBellyTestSetting);
        }
        
        activeNeighbors.AddRange(tempList);
    }

    public void ReturnRandomNeighborToPool()
    {
        int sizeActiveNeighbors = activeNeighbors.Count;
        if (sizeActiveNeighbors != 0)
        {
            int index = Random.Range(0, sizeActiveNeighbors);
            
            Neighbor neighbor = activeNeighbors[index];
            
            activeNeighbors.RemoveAt(index);
            neighbor.Deactivate();
            pool.Add(neighbor);
        }
    }

    public void ReturnToPoolAmount(int amount)
    {
        if (amount <= 0)
        {
            return;
        }
        
        int amountActuallyRemove = 0;
        List<Neighbor> neighborsToRemove = new List<Neighbor>();
        
        SetNeighborsToRemove();
        Remove();
        

        void SetNeighborsToRemove()
        {
            for (int i = 0; i < activeNeighbors.Count; i++)
            {
                if (amountActuallyRemove == amount)
                {
                    break;
                }

                neighborsToRemove.Add(activeNeighbors[i]);
                amountActuallyRemove++;
            }
        }

        void Remove()
        {
            foreach (var neighbor in neighborsToRemove )
            {
                activeNeighbors.Remove(neighbor);
                neighbor.Deactivate();
                pool.Add(neighbor);
            }
        
            Debug.Log($"Want to remove {amount} and managed to remove {amountActuallyRemove}");
        }
    }

    public void ReturnToPool(string gameObjectName)
    {
        Neighbor neighborToReturn = null;

        foreach (var neighbor in activeNeighbors)
        {
            if (gameObjectName == neighbor.name)
            {
                neighborToReturn = neighbor;
            }
        }

        if (neighborToReturn != null)
        {
            activeNeighbors.Remove(neighborToReturn);
            neighborToReturn.Deactivate();
            pool.Add(neighborToReturn);
        }
        else
        {
            Debug.Log($"Not found {gameObjectName}");
        }
        
    }
}
