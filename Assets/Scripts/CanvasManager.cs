using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    GameObject Game;
    [SerializeField]
    GameObject Shop; 
    [SerializeField]
    GameObject Profile; 

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
}
