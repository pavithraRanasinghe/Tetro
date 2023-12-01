using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class BannerAd : MonoBehaviour
{
    void Start()
    {
        MobileAds.Initialize(status => {});   
    }
    
    // These ad units are configured to always serve test ads.
    #if UNITY_ANDROID
        //For Testing
        private string _adUnitId = "ca-app-pub-3940256099942544/6300978111";
        //For Production
        // private string _adUnitId = "ca-app-pub-8877564504785602/7963682125";
    #elif UNITY_IPHONE
      private string _adUnitId = "ca-app-pub-3940256099942544/2934735716";
    #else
      private string _adUnitId = "unused";
    #endif

    BannerView _bannerView;

    private void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        if (_bannerView != null)
        {
            DestroyBannerAd();
        }

        _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);
    }
    
    public void LoadAd()
    {
        if(_bannerView == null)
        {
            CreateBannerView();
        }
        var adRequest = new AdRequest();
        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
    }
    
    public void DestroyBannerAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }
    
}
