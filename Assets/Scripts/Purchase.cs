using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Purchase : MonoBehaviour
{
    public
    CanvasManager canvasManager;
    public string name = "";
    public float cost = 0f;
    public float boost = 0f;
    public Boolean type = true;
    public Boolean buying = false;
    public int tier = 0;
    public int level = 0;
    [SerializeField]
    TextMeshProUGUI Name;
    [SerializeField]
    TextMeshProUGUI Cost;
    [SerializeField]
    TextMeshProUGUI Boost;
    [SerializeField]
    Image image;

    void Start()
    {
       
        Rewrite();

    }
    public void Rewrite()
    {
        switch (tier)
        {
            case 1:
                Name.text = name + " I";
                Name.text += "(lvl" + level + ")";
                break;
            case 2:
                Name.text = name + " II";
                Name.text += "(lvl" + level + ")";
                break;
            case 3:
                Name.text = name + " III";
                Name.text += "(lvl" + level + ")";
                break;
            default:
                Name.text = name;
                break;
        }

        string outputCost = Math.Round(cost, 2).ToString() + "₮";
        if (cost >= 1000000)
        {
            outputCost = Math.Round(cost / 1000000, 2).ToString() + "kk₮";
        }
        else if (cost >= 1000)
        {
            outputCost = Math.Round(cost / 1000, 2).ToString() + "k₮";
        }
        Cost.text = outputCost;
        string outputBoost = Math.Round(boost, 2).ToString() + "₮";
        if (boost >= 1000000)
        {
            outputBoost = "+" + Math.Round(boost / 1000000, 2).ToString() + "kk₮";
        }
        else if (boost >= 1000)
        {
            outputBoost = "+" + Math.Round(boost / 1000, 2).ToString() + "k₮";
        }
        else if(boost <= 0)
        {
            outputBoost = "";
        }
        Cost.text = outputCost;
        Boost.text = outputBoost;
        if (type)
            image.sprite = Resources.Load<Sprite>("Sprites/byclick");
        else
            image.sprite = Resources.Load<Sprite>("Sprites/persec");

        if (tier == 0)
        {
            image.sprite = Resources.Load<Sprite>("Sprites/delad");
        }
    }
    public void BuyClick()
    {
        if (!buying)
        {
            buying = true;
            if(canvasManager.global.Buy(cost, type, boost))
            {
                buying = false ;
                cost *= 2.5f;
                boost *=1.2f;
                level++;
                canvasManager.global.UpgradeUpdate(new Upgrade { Name = name, Cost= cost, Boost = boost,tier = tier, lvl = level,type = type });
                Rewrite();
            }
            else
            {
                buying = false;
            }


        }
        else canvasManager.Toast("Buying alredy began");
    }

}
