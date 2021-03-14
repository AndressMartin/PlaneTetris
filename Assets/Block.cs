using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    bool movable = true;
    float timer = 0f;
    public GameObject holder;
    public GameObject shadow;
    private GameObject myShadow;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        holder = transform.GetChild(0).gameObject;
        myShadow = Instantiate(shadow);
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
        foreach(Transform subBlock in holder.transform)
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

    void CallShadow()
    {
        myShadow.transform.position = transform.position;
        myShadow.GetComponent<Invisible>().movable = true;
    }
    void RotateShadow()
    {
        Debug.Log("Rotating");
        myShadow.transform.position = transform.position;
        myShadow.GetComponent<Invisible>().holder.transform.eulerAngles = holder.transform.eulerAngles;
        myShadow.GetComponent<Invisible>().movable = true;
    }
    void Update()
    {
        if (movable)
        {
            //Update the timer 
            timer += 1 * Time.deltaTime;
            //DROP
            if (Input.GetKey(KeyCode.DownArrow) && timer > GameManager.quickDropTime)
            {
                gameObject.transform.position -= new Vector3(0, 1, 0);
                timer = 0;
                if (!CheckValid())
                {
                    movable = false;
                    gameObject.transform.position += new Vector3(0, 1, 0);
                    RegisterBlock();
                    //gameManager.ClearLines();
                    gameManager.SpawnBlock(GameManager.nextBlock);
                    Destroy(myShadow);
                }
            }
            else if (timer > GameManager.dropTime)
            {
                gameObject.transform.position -= new Vector3(0, 1, 0);
                timer = 0;
                if (!CheckValid())
                {
                    movable = false;
                    gameObject.transform.position += new Vector3(0, 1, 0);
                    RegisterBlock();
                    //gameManager.ClearLines();
                    gameManager.SpawnBlock(GameManager.nextBlock);
                    Destroy(myShadow);
                }
            }
            //Sideways
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                gameObject.transform.position -= new Vector3(1, 0, 0);
                if (!CheckValid())
                {
                    gameObject.transform.position += new Vector3(1, 0, 0);
                }
                CallShadow();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                gameObject.transform.position += new Vector3(1, 0, 0);
                if (!CheckValid())
                {
                    gameObject.transform.position -= new Vector3(1, 0, 0);
                }
                CallShadow();
            }
            //Rotation
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                holder.transform.eulerAngles -= new Vector3(0, 0, 90);
                if (!CheckValid())
                {
                    holder.transform.eulerAngles += new Vector3(0, 0, 90);
                }
                RotateShadow();
            }
        }
        
    }
}
