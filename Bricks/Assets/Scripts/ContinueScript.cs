using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class ContinueScript : MonoBehaviour {
    private GameObject menuScriptGameObject;
    private GameObject player;


    #if UNITY_IOS
    private string gameId = "3156246";
    #elif UNITY_ANDROID
    private string gameId = "3156247";
    #endif

    bool testMode = true;
    public string placementId = "rewardedVideo";

    public void ShowAd()
    {
        StartCoroutine(WaitForAd());
    }

    public void stopGameover(){
        menuScriptGameObject.GetComponent<Menu>().wait = false;

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
            Continue();
        }
    }

	private void Start()
	{
        menuScriptGameObject = GameObject.FindWithTag("MainCamera");
        player = GameObject.FindWithTag("Player");
        Monetization.Initialize(gameId, testMode);


	}

	public void Continue(){
        Destroy(player.GetComponent<PlayerController>().collidedObstacle);
        menuScriptGameObject.GetComponent<Menu>().restartTime = 3f;
        menuScriptGameObject.GetComponent<Menu>().gameOver = false;
        menuScriptGameObject.GetComponent<Menu>().RestartMenu.SetActive(false);
        menuScriptGameObject.GetComponent<Menu>().wait = true;
        player.GetComponent<PlayerController>().enabled = true;


    }
}
