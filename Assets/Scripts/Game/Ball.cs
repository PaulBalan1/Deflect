using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ballRb2d;
    [SerializeField] private float ballSpeed = 10f;  

    void Update()
    {
        ballRb2d.velocity = ballRb2d.velocity.normalized * ballSpeed;
    }
}
