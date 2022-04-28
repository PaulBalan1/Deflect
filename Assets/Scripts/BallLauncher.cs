using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public static bool released;
    public static bool allBallsReturned;

    private List<Ball> balls = new List<Ball>();

    private Vector3 startPosition, endPosition;

    private float timer;

    private int noOfBalls;

    [Header("Views")]
    [SerializeField] private LaunchPreview launchPreview;
    [SerializeField] private BlockSpawner blockSpawner;

    [Header("Prefabs")]
    [SerializeField] private Ball ballPrefab;


    private void Awake()
    {
        released = false;
        allBallsReturned = true;
        CreateBall();
    }

    internal void ReturnBall()
    {
        noOfBalls++;
        if (noOfBalls == balls.Count)
        {
            allBallsReturned = true;
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

    private void Update()
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
            allBallsReturned = true;
            released = false;
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
            allBallsReturned = false;
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
        
        launchPreview.SetEndPoint(transform.position - direction);
    }

    private void StartDrag(Vector3 worldPosition)
    {
        startPosition = worldPosition;
        launchPreview.SetStartPoint(transform.position);
    }
}
 