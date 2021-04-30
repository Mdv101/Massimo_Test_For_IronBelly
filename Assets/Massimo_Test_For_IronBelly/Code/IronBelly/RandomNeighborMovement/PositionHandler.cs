using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is a monostate because this pattern is more test friendly that the Singletoon
public class PositionHandler
{
    private List<FindNearestNeighbour> objectsToUpdateNearestNeighbours;
    private KdTree<FindNearestNeighbour> kdTree;
    
    private static PositionHandler Instance;

    public PositionHandler(float maxSearchLength = 99)
    {
        if (Instance != null)
        {
            Instance.objectsToUpdateNearestNeighbours = null;
        }

        objectsToUpdateNearestNeighbours = new List<FindNearestNeighbour>();
        
        kdTree = new KdTree<FindNearestNeighbour>();
        kdTree.SearchLength = maxSearchLength;
        
        Instance = this;
    }

    public void UpdatePositions()
    {
        int size = objectsToUpdateNearestNeighbours.Count;

        for (int i = 0; i < size; i++)
        {
            
            FindNearestNeighbour nearestNeighbour =  kdTree.FindClosest(objectsToUpdateNearestNeighbours[i].GetPosition(), objectsToUpdateNearestNeighbours[i]);
            objectsToUpdateNearestNeighbours[i].CurrentNearestNeighbour = nearestNeighbour;
        }
    }
    
    public static void RegisterFindNearestNeighbour(FindNearestNeighbour findNearestNeighbour)
    {
        if (Instance.objectsToUpdateNearestNeighbours.Contains(findNearestNeighbour))
        {
            throw new Exception("Trying to register an already registered object ");
        }
        
        Instance.objectsToUpdateNearestNeighbours.Add(findNearestNeighbour);
        Instance.UpdateKdTree();
    }
    
    public static void DeregisterFindNearestNeighbour(FindNearestNeighbour findNearestNeighbour)
    {
        if (Instance.objectsToUpdateNearestNeighbours.Contains(findNearestNeighbour))
        {
            Instance.objectsToUpdateNearestNeighbours.Remove(findNearestNeighbour);
            Instance.UpdateKdTree();
            return;
        }
        
        throw new Exception("Trying to deregister an not registered object");
    }

    public bool ItIsRegistered(FindNearestNeighbour findNearestNeighbour)
    {
        foreach (var nearestNeighbour in objectsToUpdateNearestNeighbours)
        {
            if (nearestNeighbour == findNearestNeighbour)
            {
                return true;
            }
        }

        return false;
    }

    private void UpdateKdTree()
    {
        kdTree.Clear();
        kdTree.AddAll(objectsToUpdateNearestNeighbours);
    }
}

