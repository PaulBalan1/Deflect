using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public static bool released;
    public static bool allBallsReturned;
    private int noOfBallsReturned;
    private int ballsThisWave;

    private List<Ball> balls = new List<Ball>();

    private Vector3 startPosition, endPosition;

    private float timer;

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
        ballsThisWave = 1;
    }

    internal void ReturnBall()
    {
        noOfBallsReturned++;
        if (noOfBallsReturned == ballsThisWave)
        {
            allBallsReturned = true;
            SpawnNextWave();
        }
    }

    private void SpawnNextWave()
    {
        ballsThisWave++;
        blockSpawner.SpawnRowOfBlocks();
        blockSpawner.SpawnRowOfBlocks();
        CreateBall();
    }

    private void CreateBall()
    {
        var ball = Instantiate(ballPrefab);
        ball.transform.position = transform.position;
        balls.Add(ball);
    }

    private void Update()
    {
        if (released)
        {
            timer += Time.deltaTime;
        }

        // Timer after each active ball will disapper (used for stuck balls).
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

        // Creating ball-shooting path.
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * (-10);

        if (Input.GetMouseButtonDown(0))
        {
            StartDrag(worldPosition);
        }
        else if(Input.GetMouseButton(0))
        {
            ContinueDrag(worldPosition);
        }
        else if (Input.GetMouseButtonUp(0))
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
        
        // Checking if shooting downwards, if yes, ignoring the shot.
        if ((direction.x >= 0 && direction.y >= 0) || (direction.x <= 0 && direction.y >= 0))
        {
            released = false;
        }
        else if (direction.x != 0 || direction.y != 0)
        {
            noOfBallsReturned = 0;
            foreach (var ball in balls)
            {
                ball.transform.position = transform.position;
                ball.gameObject.SetActive(true);
                ball.GetComponent<Rigidbody2D>().AddForce(-direction);
                yield return new WaitForSeconds(0.1f);
            }
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
 