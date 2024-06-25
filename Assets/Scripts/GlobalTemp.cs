using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTemp:MonoBehaviour
{
    [SerializeField] CanvasManager canvas;
    [SerializeField] float score = 0;
    int count = 0;
    public
    float Score {
        get { return score; }
        set { score = value;
            canvas.ChangeScore(score);
        }
    }
    [SerializeField]
    float persec = 0;
    public
    float PerSec
    {
        get { return persec; }
        set
        {
            persec = value;
            canvas.ChangePerSec(persec);
        }
    }
    [SerializeField]
    float byclick = 1;
    public
    float ByClick
    {
        get { return byclick; }
        set
        {
            byclick = value;
            canvas.ChangeByClick(byclick);
        }
    }

    public void Click()
    {
        Score += byclick;
        count++;
        //canvas.ChangeScore(score);
    }
    public bool Buy(float cost, Boolean type, float boost)
    {
        if (type)
        {
            if(score - cost >= 0)
            {
                ByClick += boost;
                Score -= cost;
                return true;
            }
            else
            {
                canvas.Toast("You need " + (cost - score) + "₮");
                return false;
            }

        }
        else {
            return false; }
    }
    private void Start()
    {
        canvas.ChangeScore(Score);
        canvas.ChangeByClick(byclick);
        canvas.ChangePerSec(persec);
    }
}
