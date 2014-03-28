using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Phone.Controls;
using PhoneDirect3DXamlAppInterop;
using PhoneDirect3DXamlAppComponent.IAPHelper;


namespace PhoneDirect3DXamlAppComponent
{
    /// <summary>
    /// A callback class that implements the WinRT Component interface ICallback.
    /// All methods provided will be called throught the ICallback interface in native code.
    /// </summary>
    public class IAPCallback : ICallback
    {
        public event EventHandler<CompletedEventArgs> OnItemsRefreshed;

        private MainPage m_MainPage = null;
        private Direct3DInterop m_d3dInterop = null;
        private bool bAdCreated = false;

        public void SetMainPage(MainPage mainPage)
        {
            m_MainPage = mainPage;
        }

        public void SetDirect3DInterop(Direct3DInterop d3dInterop)
        {
            m_d3dInterop = d3dInterop;
        }

        public void GetIAP()
        {
            m_MainPage.GetIAP();          
        }

        // when return from StoreFront, receive response
        public void ReceivedResponse(string strResult)
        {
            if (OnItemsRefreshed != null && m_d3dInterop != null)
            {
                m_d3dInterop.OnIAPEvent(null, new CompletedEventArgs(true, 200, "Purchased successfully", strResult), OnItemsRefreshed);
            }
        }
    }
}