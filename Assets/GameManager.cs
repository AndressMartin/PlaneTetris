using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float dropTime = 0.8f;
    public static float quickDropTime = 0.04f;
    public static float nextBlock;
    public static int largura = 10, altura = 24;
    public GameObject[] blocos;
    public GameObject[] blocosUI;
    public GameObject currentUIBlock;
    public Transform[,] grid = new Transform[largura, altura];
    private void Awake()
    {
        
    }
    void Start()
    {
        SpawnBlock();
    }
    private void Update()
    {
        for (int y = 0; y < altura; y++)
        {
            if (IsLineComplete(y))
            {
                DestroyLine(y);
                MoveLines(y);
            }
        }
    }

    private void MoveLines(int y)
    {
        for (int f = 0; f < altura-y; f++)
        {
            for (int i = y; i < altura; i++)
            {
                for (int x = 0; x < largura; x++)
                {
                    if (grid[x, y+1] != null && y < altura)
                    {
                        grid[x, y] = grid[x, y + 1];
                        grid[x, y].gameObject.transform.position -= new Vector3(0, 1, 0);
                        grid[x, y + 1] = null;
                    }
                }
            }
            y++;
        }
        
    }

    private void DestroyLine(int y)
    {
        for (int x = 0; x < largura; x++)
        {
            Destroy(grid[x, y].gameObject);
        }
        Score.Increase();
    }

    bool IsLineComplete(int y) 
    {
        for (int x = 0; x < largura; x++)
        {
            if (grid[x,y] == null)
            {
                return false;
            }
        }
        return true;
    }
    public void SpawnBlock()
    {
        float currentBlock = Random.Range(0, 1f);
        currentBlock *= blocos.Length;
        nextBlock = Random.Range(0, 1f);
        nextBlock *= blocos.Length;
        Instantiate(blocos[Mathf.FloorToInt(currentBlock)]);
        ShowNextBlock();
    }

    public void SpawnBlock(float _currentBlock)
    {
        _currentBlock = nextBlock;
        nextBlock = Random.Range(0, 1f);
        nextBlock *= blocos.Length;

        while (Mathf.FloorToInt(_currentBlock) == Mathf.FloorToInt(nextBlock))
        {
            nextBlock = Random.Range(0, 1f);
        }
        DestroyUIBlock();
        Instantiate(blocos[Mathf.FloorToInt(_currentBlock)]);
        ShowNextBlock();
    }

    
    private void ShowNextBlock()
    {
        currentUIBlock = Instantiate(blocosUI[Mathf.FloorToInt(nextBlock)]);
    }
    private void DestroyUIBlock()
    {
        Destroy(currentUIBlock);
    }



}
