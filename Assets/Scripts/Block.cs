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

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        holder = transform.GetChild(0).gameObject;
        if (GameManager.ShadowBlockEnabled) myShadow = Instantiate(shadow);
    }
    void Update()
    {
        if (movable && !GameManager.Lost)
        {
            timer += 1 * Time.deltaTime;
            Drop();
            MoveSideways();
            Rotate();
        }
        if (holder.transform.childCount == 0)
        {
            Destroy(holder.transform.parent.gameObject);
        }
    }
    private void Rotate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            holder.transform.eulerAngles -= new Vector3(0, 0, 90);
            if (!CheckBlock())
            {
                holder.transform.eulerAngles += new Vector3(0, 0, 90);
            }
            if (GameManager.ShadowBlockEnabled) RotateShadow();
        }
    }
    private void MoveSideways()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameObject.transform.position -= new Vector3(1, 0, 0);
            if (!CheckBlock())
            {
                gameObject.transform.position += new Vector3(1, 0, 0);
            }
            if (GameManager.ShadowBlockEnabled) CallShadow();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameObject.transform.position += new Vector3(1, 0, 0);
            if (!CheckBlock())
            {
                gameObject.transform.position -= new Vector3(1, 0, 0);
            }
            if (GameManager.ShadowBlockEnabled) CallShadow();
        }
    }
    private void Drop()
    {
        if (Input.GetKey(KeyCode.DownArrow) && timer > GameManager.quickDropTime)
        {
            gameObject.transform.position -= new Vector3(0, 1, 0);
            timer = 0;
            if (!CheckBlock())
            {
                movable = false;
                gameObject.transform.position += new Vector3(0, 1, 0);
                RegisterBlock();
                ClickSoundPlayer.PlayCLickSound();
                gameManager.SpawnBlock(GameManager.nextBlock);
                if (GameManager.ShadowBlockEnabled) Destroy(myShadow);
            }
        }
        else if (timer > GameManager.dropTime)
        {
            gameObject.transform.position -= new Vector3(0, 1, 0);
            timer = 0;
            if (!CheckBlock())
            {
                movable = false;
                gameObject.transform.position += new Vector3(0, 1, 0);
                RegisterBlock();
                ClickSoundPlayer.PlayCLickSound();
                gameManager.SpawnBlock(GameManager.nextBlock);
                if (GameManager.ShadowBlockEnabled) Destroy(myShadow);
            }
        }
    }
    void RegisterBlock() 
    {
        foreach (Transform subBlock in holder.transform)
        {
            if (Mathf.FloorToInt(subBlock.position.y) < GameManager.altura)
                gameManager.grid[Mathf.FloorToInt(subBlock.position.x),
                Mathf.FloorToInt(subBlock.position.y)] = subBlock;
            else GameManager.Lost = true;
        }
    }
    bool CheckBlock()
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
    void CallShadow()
    {
        myShadow.transform.position = transform.position;
        myShadow.GetComponent<Invisible>().movable = true;
    }
    void RotateShadow()
    {
        myShadow.transform.position = transform.position;
        myShadow.GetComponent<Invisible>().holder.transform.eulerAngles = holder.transform.eulerAngles;
        myShadow.GetComponent<Invisible>().movable = true;
    }
    

    
}
