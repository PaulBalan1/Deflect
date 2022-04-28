using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private List<Block> blocksSpawned = new List<Block>();

    [SerializeField] private Transform bottomTrigger;
    [SerializeField] private Block blockPrefab;

    private float distanceBetweenBlocks = 0.75f;

    private int columns = 7;
    private int rowNumber;


    private void Start()
    {
        SpawnRowOfBlocks();
    }


    public void SpawnRowOfBlocks()
    {
        for (int j = 0; j < 1; j++)
        {
            foreach (var block in blocksSpawned)
            {
                if (block != null)
                {
                    block.transform.position += Vector3.down * distanceBetweenBlocks;
                    if (block.transform.position.y <= bottomTrigger.position.y + 1)
                    {
                         Initiate.Fade("Menu", Color.black, 5.0f);
                    }
                }
            }

            for (int i = 0; i < columns; i++)
            {
                if (Random.Range(0, 100) <= 35)
                {
                    var block = Instantiate(blockPrefab, CalculateBlockPosition(i), Quaternion.identity);
                    int life = Random.Range(1, 3) + rowNumber;
                    block.SetHits(life);
                    blocksSpawned.Add(block);
                }
            }
            rowNumber++;
        }
    }

    private Vector3 CalculateBlockPosition(int i)
    {
        Vector3 position = transform.position;
        position += Vector3.right * i * distanceBetweenBlocks;
        return position;
    }
}
