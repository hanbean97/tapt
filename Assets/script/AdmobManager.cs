using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class AdmobManager : MonoBehaviour
{
    public bool isTestmode;
    public Text LogText;
    public Button FrontAdsBt, RewardAdsBtn;

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
