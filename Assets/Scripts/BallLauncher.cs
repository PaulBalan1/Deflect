using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{

    private Vector3 startPosition, endPosition;
    private Launcher launchPreview;
    private BlockSpawner blockSpawner;
    private float timer;
    public static Boolean released;

    private List<Ball> balls = new List<Ball>();
    private int noOfBalls;

    [SerializeField]
    private Ball ballPrefab;

    private void Awake()
    {
        released = false;
        launchPreview = GetComponent<Launcher>();
        blockSpawner = FindObjectOfType<BlockSpawner>();
        CreateBall();
    }

    internal void ReturnBall()
    {
        noOfBalls++;
        if (noOfBalls == balls.Count)
        {
            blockSpawner.SpawnRowOfBlocks();
            blockSpawner.SpawnRowOfBlocks();
            CreateBall();
            CreateBall();
        }
    }

    private void CreateBall()
    {
        var ball = Instantiate(ballPrefab);
        ball.transform.position = transform.position;
        balls.Add(ball);
        noOfBalls++;
    }

    void Update()
    {
        if (released)
        {
            timer += Time.deltaTime;
        }

        if (timer > 20)
        {
           foreach(var ball in balls){
                ball.gameObject.SetActive(false);
            }
            timer = 0;
        }

        if (released) return; 
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * (-10);

        if (Input.GetMouseButtonDown(0))
        {
            StartDrag(worldPosition);
        }else if(Input.GetMouseButton(0))
        {
            ContinueDrag(worldPosition);
        }else if (Input.GetMouseButtonUp(0))
        {
            released = true;
            EndDrag(worldPosition);
            timer = 0;
        }
    }

    private void EndDrag(Vector3 worldPosition)
    {
        StartCoroutine(LaunchBalls());
    }

    private IEnumerator LaunchBalls()
    {
        Vector3 direction = endPosition - startPosition;
        direction.Normalize();
        if (direction.x == 0 && direction.y == 0) released = false;
        if (direction.x != 0 || direction.y != 0)
        {
            foreach (var ball in balls)
            {
                ball.transform.position = transform.position;
                ball.gameObject.SetActive(true);
                ball.GetComponent<Rigidbody2D>().AddForce(-direction);
                yield return new WaitForSeconds(0.1f);
            }
            noOfBalls = 0;
        }
    }

    private void ContinueDrag(Vector3 worldPosition)
    {
        endPosition = worldPosition;
        Vector3 direction = endPosition - startPosition;
        
        launchPreview.setEndPoint(transform.position - direction);
    }

    private void StartDrag(Vector3 worldPosition)
    {
        startPosition = worldPosition;
        launchPreview.setStartPoint(transform.position);
    }
}
 