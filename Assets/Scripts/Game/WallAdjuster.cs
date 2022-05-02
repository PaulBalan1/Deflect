using System;
using UnityEngine;

public class WallAdjuster : MonoBehaviour
{
    [SerializeField] private Transform top;
    [SerializeField] private Transform right;
    [SerializeField] private Transform left;
    [SerializeField] private Transform bottom;

    public static event Action OnWallsAdjusted;

    private void Start()
    {
        AdjustWalls();
    }

    // Adjust position of walls depending on device screen size.
    private void AdjustWalls()
    {
        top.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1, 0));
        right.position = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
        left.position = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
        bottom.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, 0));
        OnWallsAdjusted?.Invoke();
    }
}
