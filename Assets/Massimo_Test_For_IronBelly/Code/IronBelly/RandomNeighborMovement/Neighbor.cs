using System;
using UnityEngine;

public class Neighbor : MonoBehaviour
{
    [SerializeField] private FindNearestNeighbour findNearestNeighbour;
    [SerializeField] private RandomMove randomMove;

    public static event Action<Neighbor> OnNeighborEnabled;

    public void Initialize(IronBellyTestSetting ironBellyTestSetting)
    {
        AreaOfMove areaOfMove = ironBellyTestSetting.areaOfMove;
        transform.position = areaOfMove.GetRandomPointInArea();
        
        randomMove.Initialize(areaOfMove);
        
        findNearestNeighbour.Initialize(ironBellyTestSetting.useLineRender);
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        OnNeighborEnabled?.Invoke(this);
    }

    public void Deactivate()
    {
        findNearestNeighbour.Deregister();
        gameObject.SetActive(false);
    }
}
