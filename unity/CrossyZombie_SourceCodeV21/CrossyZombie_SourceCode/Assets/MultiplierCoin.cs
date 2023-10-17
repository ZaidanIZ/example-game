using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierCoin : MonoBehaviour
{
    public GameController controller;
    int coindariAds;
    int DiamondDariAds;
    public Button[] tombol;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ShowRewardedCoin(200);   
    }

  public   void ShowRewardedCoin(int jmlhcoin)
    {
        coindariAds = jmlhcoin;
        ADsummoner.adSummoner.ShowReward(GetCoin);

    }

    public void ShowRewardDiamond(int jmlhDiamond)
    {
        DiamondDariAds = jmlhDiamond;
        ADsummoner.adSummoner.ShowReward(GetDiamond);
    }

    void GetDiamond()
    {
        Debug.Log("getDiamond");
        controller.Diamond += DiamondDariAds;
        PlayerPrefs.SetFloat("Diamond", controller.Diamond);
        UIManager.Instance.diamondInShopText.text = controller.Diamond.ToString();
        AfterRewardShop();
    }



    //string c = "coin";
    void GetCoin()
    {
        Debug.Log("getCoin");
        controller.Coin += coindariAds;
        PlayerPrefs.SetFloat("coin", controller.Coin);
        UIManager.Instance.coinInShopText.text = controller.Coin.ToString();
        AfterRewardShop();
    }

    public void AfterRewardShop() {
        Debug.Log("sembarang");

        foreach (Button item in tombol)
        {
            item.interactable = false;
        }

    }

    public void LoadRewardShop()
    {
        ADsummoner.adSummoner.LoadReward(AfterLoad);
    }

    public void AfterLoad()
    {
        foreach (Button item in tombol)
        {
            item.interactable = true;
        }
    }
     
    
}
