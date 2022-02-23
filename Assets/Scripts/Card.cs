using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int index;

    int x = 0; 
    int y = 0;

    private SpriteRenderer _spriteRenderer;

    private Action<int, int> swapFunction = null;
    private void Awake()
    {
        _spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
    }

    public void Init(int i, int j, int index, Sprite sprite, Action<int, int> swapFunction)
    {
        this.index = index;
        _spriteRenderer.sprite = sprite;
        x = i;
        y = j;
        this.swapFunction = swapFunction;
    }

    public void UpdatePos(int i, int j)
    {
        x = i;
        y = j;
        this.gameObject.transform.position = new Vector2(x, y);
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && swapFunction!= null)
        {
            swapFunction(x, y);
        }
    }


    internal bool IsEmpty()
    {
        return index == 4;

    }
    void Update()
    {


        //if i have time

        ////Gets the world position of the mouse on the screen        
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        ////Checks whether the mouse is over the sprite
        //bool overSprite = _spriteRenderer.bounds.Contains(mousePosition);

        ////If it's over the sprite
        //if (overSprite)
        //{
        //    //If we've pressed down on the mouse (or touched on the iphone)
        //    if (Input.GetButton("Fire1"))
        //    {
        //        //Set the position to the mouse position
        //        this.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
        //                                            Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
        //                                              0.0f);
        //    }
        //    //released
        //    else
        //    {

        //    }
        //}



    }

}
