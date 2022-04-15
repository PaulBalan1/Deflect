using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 dragStartPoint;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            lineRenderer.enabled = true;
        else if(Input.GetMouseButtonUp(0))
            lineRenderer.enabled = false;
    }

    public void setStartPoint(Vector3 worldPosition)
    {
        
        dragStartPoint = worldPosition;
        lineRenderer.SetPosition(0, dragStartPoint);
    }

    public void setEndPoint(Vector3 worldPosition)
    {
        Vector3 offset = worldPosition - dragStartPoint;
        //Debug.Log(offset.x + " " + offset.y);
        
        Vector3 dragEndPoint = transform.position + offset;

        lineRenderer.SetPosition(1, dragEndPoint); 
    }

}
