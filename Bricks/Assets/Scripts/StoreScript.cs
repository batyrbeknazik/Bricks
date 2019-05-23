using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;

public class StoreScript : MonoBehaviour
{
    protected Menu menu;
    public Material[] material;
    public Button[] buyButton;
    #if UNITY_IOS
    private string gameId = "3156246";
#elif UNITY_ANDROID
    private string gameId = "3156247";
#endif

    bool testMode = true;
    public string placementId = "rewardedVideo";

	private void Start()
	{
        menu = GameObject.FindWithTag("MainCamera").GetComponent<Menu>();
	}
	public void StoreButton()
    {
        menu.store.GetComponent<Animator>().Play("Store", 0, 0f);
        menu.mainMenu.GetComponent<Animator>().Play("Menu", 0, 0f);


    }
    public void StoreButtonBack(){
        menu.store.GetComponent<Animator>().Play("StoreBack", 0, 0f);
        menu.mainMenu.GetComponent<Animator>().Play("MenuBack", 0, 0f);

    }


    public void BuyButton(int index){
        if(index!=1){
            menu.cube.GetComponent<MeshRenderer>().material = material[index];
        }

        else{
            ShowAd();
        }
    }

    public void purchaseFailed(){
        Debug.Log("Failed Bitch");
    }



    public void ShowAd()
    {
        StartCoroutine(WaitForAd());
    }

    IEnumerator WaitForAd()
    {
        while (!Monetization.IsReady(placementId))
        {
            yield return null;
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show(AdFinished);
        }
    }

    void AdFinished(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            //reward player
            menu.cube.GetComponent<MeshRenderer>().material = material[1];
            menu.cube.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
;
        }
    }


}
