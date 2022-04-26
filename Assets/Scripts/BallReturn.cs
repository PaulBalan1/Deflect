using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallReturn : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        FindObjectOfType<BallLauncher>().ReturnBall();
        collision.collider.gameObject.SetActive(false);
        if (BallLauncher.allBallsReturned) BallLauncher.released = false;
    }
}
