using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlobalTemp:MonoBehaviour
{
    [SerializeField] CanvasManager canvas;
    [SerializeField] float score = 1;
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
    float byclick = 0;
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
    FirebaseFirestore db;



    public void UpgradeUpdate(Upgrade upgrade)
    {
        if(upgrade.type)
            db.Collection("Upgrades").Document("ByClick"+upgrade.tier).SetAsync(upgrade).ContinueWithOnMainThread(task =>
            {
                Debug.Log("Upgrade update");
            });
    }
    public void GetUpgrade()
    {
        List<Upgrade> result = new List<Upgrade>();
        db.Collection("Upgrades").GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            foreach (var item in task.Result.Documents)
            {
                result.Add(item.ConvertTo<Upgrade>());
            }
            canvas.upgrades = result.ToArray();
        });
        
    }

    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        db.Collection("users").Document("user").GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            User user = task.Result.ConvertTo<User>();
            Score = user.Score;
            ByClick = user.ByClick;
            PerSec = user.PerSec;
            
        });
        canvas.ChangeScore(Score);
        canvas.ChangeByClick(ByClick);
        canvas.ChangePerSec(PerSec);
    }

    public void Click()
    {
       
        Score += byclick;
        count++;
        User user = new User
        {
            Score = Score,
            PerSec = PerSec,
            ByClick = ByClick
        };
        db.Collection("users").Document("user").SetAsync(user).ContinueWithOnMainThread(task =>
        {
            Debug.Log("updated");
        });
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
}
