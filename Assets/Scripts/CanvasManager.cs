using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using Unity.VisualScripting;

public class CanvasManager : MonoBehaviour
{
    public GlobalTemp global;
    [SerializeField]
    GameObject Game;
    [SerializeField]
    GameObject Shop; 
    [SerializeField]
    GameObject Profile; 
    [SerializeField]
    TextMeshProUGUI Score;
    [SerializeField]
    TextMeshProUGUI PerSec; 
    [SerializeField]
    TextMeshProUGUI ByClick;
    [SerializeField]
    GameObject ToastText;

    [SerializeField]
    GameObject ToastPanel;

    [SerializeField]
    GameObject upgrade;

    [SerializeField]
    GameObject ScrollViewContent;
    public Upgrade[] upgrades = null;

    private void Start()
    {
        StartCoroutine(starting());
    }
    IEnumerator starting()
    {
        yield return new WaitForSeconds(3);
        global.GetUpgrade();
        yield return new WaitForSeconds(3);
        if (upgrades != null)
        {
            int c = 0;
            foreach (var item in upgrades)
            {
                Vector3 scr = ScrollViewContent.transform.position;
                GameObject i = Instantiate(upgrade, scr + new Vector3(520, -c * 300 - 200), Quaternion.identity, ScrollViewContent.transform);
                Purchase purchase = i.GetComponent<Purchase>();
                purchase.level = item.lvl;
                purchase.boost= item.Boost;
                purchase.cost = item.Cost;
                purchase.name = item.Name;
                purchase.tier = item.tier;
                purchase.type = item.type;
                purchase.Rewrite();
                purchase.canvasManager = this;
                c++;
            }
        }
    }
    public void ChangeScore(float score)
    {
        string output = Math.Round(score, 2).ToString();
        if (score >= 1000000)
        {
            output = Math.Round(score / 1000000, 2).ToString() + "kk";
        }
        else if (score >= 1000)
        {
            output = Math.Round(score / 1000, 2).ToString() + "k";
        }
        


        Score.text = "Score: " + output + "₮";
    }
    public void ChangePerSec(float persec)
    {
        string output = Math.Round(persec, 2).ToString();
        if (persec >= 1000000)
        {
            persec = persec / 1000000;
            output = Math.Round(persec, 2).ToString() + "kk";
        }
        else if (persec >= 1000)
        {
            persec = persec / 1000;
            output = Math.Round(persec, 2).ToString() + "k";
        }
       


        PerSec.text = "PerSec: " + output + "₮";
    }
    public void ChangeByClick(float byclick)
    {
        string output = Math.Round(byclick, 2).ToString();
        if (byclick >= 1000000)
        {
            byclick = byclick / 1000000;
            output = Math.Round(byclick, 2).ToString() + "kk";
        }
        else if (byclick >= 1000)
        {
            byclick = byclick / 1000;
            output = Math.Round(byclick, 2).ToString() + "k";
        }
        


        ByClick.text = "ByClick: " + output + "₮";
    }


    public void Click()
    {
        global.Click();
    }

    public void toShop()
    {
        Game.SetActive(false);
        Profile.SetActive(false);
        Shop.SetActive(true);
    }
    public void toGame()
    {
        Game.SetActive(true);
        Profile.SetActive(false);
        Shop.SetActive(false);

    }
    public void toProfile()
    {
        Game.SetActive(false);
        Profile.SetActive(true);
        Shop.SetActive(false);
    }
    public void Toast(string message)
    {
        //382.2
        //240
        StartCoroutine(toast(message));

    }
    IEnumerator toast(string message)
    {
        Image rect = ToastPanel.GetComponent<Image>();
        ToastPanel.SetActive(true);
        while (rect.fillAmount < 1)
        {
            rect.fillAmount += 0.02f;
            if (rect.fillAmount > 0.5f) { ToastText.SetActive(true); }
            yield return new WaitForSeconds(0.01f);
        }
        ;
        ToastText.GetComponent<TextMeshProUGUI>().text = message;
        yield return new WaitForSeconds(0.75f);
        ToastText.GetComponent<TextMeshProUGUI>().text = "";
        ToastText.SetActive(false);
        while (rect.fillAmount > 0)
        {
            rect.fillAmount -= 0.02f;
            if (rect.fillAmount < 0.5f) ToastText.SetActive(false);
            yield return new WaitForSeconds(0.01f);
        }
        ToastPanel.SetActive(false);
    }

}
