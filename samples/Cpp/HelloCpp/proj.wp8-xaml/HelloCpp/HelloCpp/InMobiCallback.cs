using InMobi.WP.AdSDK;
using PhoneDirect3DXamlAppComponent.InMobiHelper;
using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Phone.Controls;
using PhoneDirect3DXamlAppInterop;

namespace PhoneDirect3DXamlAppComponent
{
    /// <summary>
    /// A callback class that implements the WinRT Component interface ICallback.
    /// All methods provided will be called throught the ICallback interface in native code.
    /// </summary>
    public class InMobiCallback : ICallback
    {
        public event EventHandler<CompletedEventArgs> OnBannerReceived;
        public event EventHandler<CompletedEventArgs> OnBannerReceivedFailed;

        private MainPage m_MainPage = null;
        private Direct3DInterop m_d3dInterop = null;

        public void SetMainPage(MainPage mainPage)
        {
            m_MainPage = mainPage;
        }

        public void SetDirect3DInterop(Direct3DInterop d3dInterop)
        {
            m_d3dInterop = d3dInterop;
        }

        public void SwitchBottomBar()
        {
            m_MainPage.SwitchBottomBar();
            CreateBannerAd();
        }

        //Create the Ad at runtime and add to the container
        public void CreateBannerAd()
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                SDKUtility.LogLevel = LogLevels.IMLogLevelDebug;

                IMAdView AdView = new IMAdView();

                AdView.AdSize = IMAdView.INMOBI_AD_UNIT_320X50;

                //Subscribe for IMAdView events
                AdView.OnAdRequestFailed += AdView_AdRequestFailed;
                AdView.OnAdRequestLoaded += AdView_AdRequestLoaded;
                AdView.OnDismissAdScreen += new EventHandler(AdView_DismissFullAdScreen);
                AdView.OnLeaveApplication += new EventHandler(AdView_LeaveApplication);
                AdView.OnShowAdScreen += new EventHandler(AdView_ShowFullAdScreen);


                //Set the AppId. Provide you AppId
                AdView.AppId = "d187772816004152b8b93e37577da01f";
                AdView.RefreshInterval = 20;
                AdView.AnimationType = IMAdAnimationType.SLIDE_IN_LEFT;
                IMAdRequest imAdRequest = new IMAdRequest();

                AdView.LoadNewAd(imAdRequest);
                m_MainPage.AddBannerAd(AdView);
            });

        }
        // other call sdk function...

        void AdView_AdRequestFailed(object sender, IMAdViewErrorEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(e.ErrorCode.ToString() + e.ErrorDescription.ToString());
            //if (OnBannerReceivedFailed != null)
            //{
            //    OnBannerReceivedFailed(sender, new CompletedEventArgs(true, (int)errorCode.ErrorCode,h "Failed to receive ad with error " + errorCode.ToString()));
            //}

            if (OnBannerReceivedFailed != null && m_d3dInterop != null)
            {
                m_d3dInterop.OnInMobiEvent(sender, new CompletedEventArgs(true, (int)e.ErrorCode, "Received ad failed" + e.ErrorDescription), OnBannerReceivedFailed);
            }
        }

        void AdView_AdRequestLoaded(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Ad request loaded.");
            //if (OnBannerReceived != null)
            //{
            //    OnBannerReceived(sender, new CompletedEventArgs(true, 200, "Received ad successfully"));                
            //}

            if (OnBannerReceived != null && m_d3dInterop != null)
            {
                m_d3dInterop.OnInMobiEvent(sender, new CompletedEventArgs(true, 200, "Received ad successfully"), OnBannerReceived);
            }
        }

        //Invoked when the full screen Ad has been opened
        void AdView_ShowFullAdScreen(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Displaying full screen");
        }

        //Invoked when navigating out of application as Click To Action on IMAdView 
        void AdView_LeaveApplication(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Moving out of application");
        }

        //Invoked when full screen Ad displayed is closed
        void AdView_DismissFullAdScreen(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Full screen closed");
        }


    }
}