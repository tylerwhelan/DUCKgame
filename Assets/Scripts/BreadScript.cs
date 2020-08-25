using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadScript : MonoBehaviour
{
    private int breadLevel;
    public Sprite breadSprite;
    public Sprite[] breadSpriteList;
    public int BreadLevel
    {
        set
        {
            breadLevel = value;
            UpdateBread();
        }
    }

    void UpdateBread()
    {

    }

    public void ConsumeBread()
    {

    }
}
