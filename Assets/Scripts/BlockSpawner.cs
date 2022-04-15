using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private Block blockPrefab;
    private int columns = 7;
    private float distanceBetweenBlocks = 0.75f;
    private int rowNumber;

    private List<Block> blocksSpawned = new List<Block>();

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
                if (block.transform.position.y <= GameObject.FindGameObjectWithTag("Bottom").transform.position.y + 1)
                {
                     Initiate.Fade("MainMenu", Color.black, 5.0f);
                }
            }
        }

        for (int i = 0; i < columns; i++)
        {
            if (UnityEngine.Random.Range(0, 100) <= 35)
            {
                var block = Instantiate(blockPrefab, GetPosition(i), Quaternion.identity);
                int life = UnityEngine.Random.Range(1, 3) + rowNumber;
                block.SetHits(life);
                blocksSpawned.Add(block);
            }
        }
        rowNumber++;
    }
    }

    private Vector3 GetPosition(int i)
    {
        Vector3 position = transform.position;
        position += Vector3.right * i * distanceBetweenBlocks;
        return position;
    }
}
