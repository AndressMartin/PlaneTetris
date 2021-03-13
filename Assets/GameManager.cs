using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float dropTime = 0.8f;
    public static float quickDropTime = 0.04f;
    public static int larg = 10, alt = 24;
    public GameObject[] blocks;
    public Transform[,] grid = new Transform[larg, alt];

    private void Awake()
    {
        
    }
    void Start()
    {
        SpawnBlock();
    }
    private void Update()
    {
        for (int y = 0; y < alt; y++)
        {
            if (IsLineComplete(y))
            {
                DestroyLine(y);
                MoveLines(y);
            }
        }
    }
    //public void ClearLines()
    //{
    //    for (int y = 0; y < alt; y++)
    //    {
    //        if (IsLineComplete(y))
    //        {
    //            DestroyLine(y);
    //            MoveLines(y);
    //        }
    //    }
    //}

    private void MoveLines(int y)
    {
        for (int f = 0; f < alt-y; f++)
        {
            for (int i = y; i < alt; i++)
            {
                for (int x = 0; x < larg; x++)
                {
                    if (grid[x, y+1] != null && y < alt)
                    {
                        grid[x, y] = grid[x, y + 1];
                        grid[x, y].gameObject.transform.position -= new Vector3(0, 1, 0);
                        grid[x, y + 1] = null;
                    }
                }
            }
            y++;
            Debug.Log($"y is {y}");
        }
        
    }

    private void DestroyLine(int y)
    {
        for (int x = 0; x < larg; x++)
        {
            //Debug.LogWarning($"{grid[x, y].position} is destroyed.");

            Destroy(grid[x, y].gameObject);
            //grid[x, y] = null;
        }
    }

    bool IsLineComplete(int y) 
    {
        for (int x = 0; x < larg; x++)
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
        float guess = Random.Range(0, 1f);
        guess *= blocks.Length;
        Instantiate(blocks[Mathf.FloorToInt(guess)]);
    }
}
