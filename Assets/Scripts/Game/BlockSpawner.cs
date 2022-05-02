using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public static event System.Action OnGameLost;

    private List<Block> blocksSpawned = new List<Block>();

    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform rightWall;

    [SerializeField] private Block blockPrefab;

    [SerializeField] private Transform bottomTrigger;

    private float distanceBetweenBlocks = 0.75f;
    private float lengthBetweenWalls;

    private int columns = 7;
    private int rowNumber;

    private void Awake()
    {
        WallAdjuster.OnWallsAdjusted += InitializeBlockSpawner;    
    }

    private void OnDestroy()
    {
        WallAdjuster.OnWallsAdjusted -= InitializeBlockSpawner;
    }

    private void InitializeBlockSpawner()
    {
        ScaleBlocksToFitScreen();
        SpawnRowOfBlocks();
    }

    private void ScaleBlocksToFitScreen()
    {
        // Rescaling of the block size based on the screen size.
        var blockWidth = blockPrefab.transform.localScale.x / 200;
        transform.position = new Vector2(leftWall.position.x + blockWidth, transform.position.y - blockWidth);
        lengthBetweenWalls = Mathf.Abs(leftWall.position.x) + Mathf.Abs(rightWall.position.x);
    }

    public void SpawnRowOfBlocks()
    {
        foreach (var block in blocksSpawned)
        {
            if (block != null)
            {
                block.transform.position += Vector3.down * distanceBetweenBlocks;
                if (block.transform.position.y <= bottomTrigger.position.y + 1)
                {
                    var audioListener = GameObject.FindGameObjectWithTag("Audio").gameObject;
                    if(audioListener)
                        Destroy(audioListener);

                    OnGameLost?.Invoke();
                    return;
                }
            }
        }

        // Makes it less likely that the first wave will have no blocks spawned.
        int firstRoundBias;
        if (rowNumber == 0) firstRoundBias = 30;
        else firstRoundBias = 0;

        for (int i = 0; i < columns; i++)
        {
            
            if (Random.Range(0, 100) <= 35 + firstRoundBias)
            {
                var block = Instantiate(blockPrefab, CalculateBlockPosition(i), Quaternion.identity);
                int hitsRemaining = Random.Range(1, 3) + rowNumber;
                block.UpdateBlock(hitsRemaining);
                blocksSpawned.Add(block);
            }
        }
        rowNumber++;
    }

    private Vector3 CalculateBlockPosition(int i)
    {
        Vector3 position = transform.position;
        position += (lengthBetweenWalls/7f) * i * Vector3.right;
        return position;
    }
}
