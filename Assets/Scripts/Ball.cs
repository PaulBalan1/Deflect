using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField]
    private float ballSpeed = 10f;  

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb2D.velocity = rb2D.velocity.normalized * ballSpeed;
    }
}
