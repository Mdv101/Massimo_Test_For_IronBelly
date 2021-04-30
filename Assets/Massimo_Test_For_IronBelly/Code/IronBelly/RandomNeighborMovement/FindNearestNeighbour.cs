using System;
using UnityEngine;

public class FindNearestNeighbour : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    private Action DrawLine;
    public FindNearestNeighbour CurrentNearestNeighbour { get; set; }

    public void Initialize(bool useLineRenderer)
    {
        CurrentNearestNeighbour = this;

        SetDrawLine(useLineRenderer);
    }
    
    private void OnEnable()
    {
        PositionHandler.RegisterFindNearestNeighbour(this);
    }

    public void Deregister()
    {
        PositionHandler.DeregisterFindNearestNeighbour(this);
    }

    private void Update()
    {
        if (CurrentNearestNeighbour == this || CurrentNearestNeighbour == null)
        {
            lineRenderer.enabled = false;
            return;
        }
        
        DrawLine?.Invoke();
    }
    
    private void SetDrawLine(bool useLineRenderer)
    {
        DrawLine = null;
        if (useLineRenderer)
        {
            DrawLine = DrawLineUsingLineRenderer;
        }
        else
        {
            DrawLine = DrawDebugLine;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    private void DrawLineUsingLineRenderer()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0,transform.position);
        lineRenderer.SetPosition(1,CurrentNearestNeighbour.GetPosition());
    }

    private void DrawDebugLine()
    {
        Debug.DrawLine(transform.position, CurrentNearestNeighbour.GetPosition());
    }
    
}
