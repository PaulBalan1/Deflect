using UnityEngine;

public class LaunchPreview : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    private Vector3 dragStartPoint;

    private void Awake()
    {
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            lineRenderer.enabled = true;
        else if(Input.GetMouseButtonUp(0))
            lineRenderer.enabled = false;
    }

    public void SetStartPoint(Vector3 worldPosition)
    {
        dragStartPoint = worldPosition;
        lineRenderer.SetPosition(0, dragStartPoint);
    }

    public void SetEndPoint(Vector3 worldPosition)
    {
        Vector3 offset = worldPosition - dragStartPoint;
        
        Vector3 dragEndPoint = transform.position + offset;

        lineRenderer.SetPosition(1, dragEndPoint); 
    }
}
