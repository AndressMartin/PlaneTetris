using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : MonoBehaviour
{
    public bool movable = true;
    public GameObject holder;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        holder = transform.GetChild(0).gameObject;
    }
    void RegisterBlock()
    {
        foreach (Transform subBlock in holder.transform)
        {
            if (Mathf.FloorToInt(subBlock.position.y) < GameManager.altura)
                gameManager.grid[Mathf.FloorToInt(subBlock.position.x),
                Mathf.FloorToInt(subBlock.position.y)] = subBlock;
            else Debug.Log("YOU LOST");
        }
    }

    bool CheckValid()
    {
        foreach (Transform subBlock in holder.transform)
        {
            if (subBlock.transform.position.x >= GameManager.largura ||
                subBlock.transform.position.x < 0 ||
                subBlock.transform.position.y < 0)
            {
                return false;
            }
            if (subBlock.position.y < GameManager.altura &&
                gameManager.grid[Mathf.FloorToInt(subBlock.position.x),
               Mathf.FloorToInt(subBlock.position.y)] != null)
            {
                return false;
            }
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        foreach (Transform subBlock in holder.transform)
        {
            if (!movable)
                subBlock.GetComponent<MeshRenderer>().enabled = true;
            else
                subBlock.GetComponent<MeshRenderer>().enabled = false;
        }
        
        while (movable)
        {
            //Update the timer 
            //DROP
            gameObject.transform.position -= new Vector3(0, 1, 0);
            if (!CheckValid())
            {
                movable = false;
                gameObject.transform.position += new Vector3(0, 1, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            movable = false;
        }
    }
    }
