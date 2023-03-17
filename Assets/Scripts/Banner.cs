using UnityEngine;
using UnityEngine.Advertisements;

public class Banner : MonoBehaviour
{

    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;
    
    public string adUnitId = null; 
 
    void Start()
    {
        Advertisement.Banner.SetPosition(_bannerPosition);
        LoadBanner();
    }
    void LoadBanner()
    {
        var options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded
        };

        Advertisement.Banner.Load(adUnitId, options);
    }
    void OnBannerLoaded()
    {
       ShowBannerAd();     
    }
    
    void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        Advertisement.Banner.Show(adUnitId, options);
    }
    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }
    
}