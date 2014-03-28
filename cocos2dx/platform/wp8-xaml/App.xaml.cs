using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
#if DEBUG
using MockIAPLib;
using Store = MockIAPLib;
#else
using Windows.ApplicationModel.Store;
using Store = Windows.ApplicationModel.Store;
#endif

namespace PhoneDirect3DXamlAppInterop
{
    public partial class App : Application
    {
        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions.
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode,
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

            SetupMockIAP();

        }

        private void SetupMockIAP()
        {
#if DEBUG
            MockIAP.Init();
            MockIAP.RunInMockMode(true);
            MockIAP.SetListingInformation(1, "en-us", "Some description", "1", "TestApp");

            // Add some more items manually.

            ProductListing p = new ProductListing
            {
                Name = "img.1",
                ImageUri = new Uri("/Res/Image/1.png", UriKind.Relative),
                ProductId = "img.1",
                ProductType = Windows.ApplicationModel.Store.ProductType.Durable,
                Keywords = new string[] { "image" },
                Description = "Nice image",
                FormattedPrice = "1.0",
                Tag = string.Empty
            };

            MockIAP.AddProductListing("img.1", p);

            p = new ProductListing
            {
                Name = "img.2",
                ImageUri = new Uri("/Res/Image/2.png", UriKind.Relative),
                ProductId = "img.2",
                ProductType = Windows.ApplicationModel.Store.ProductType.Durable,
                Keywords = new string[] { "image" },
                Description = "Nice image",
                FormattedPrice = "1.0",
                Tag = string.Empty
            };

            MockIAP.AddProductListing("img.2", p);

            p = new ProductListing
            {
                Name = "img.3",
                ImageUri = new Uri("/Res/Image/3.png", UriKind.Relative),
                ProductId = "img.3",
                ProductType = Windows.ApplicationModel.Store.ProductType.Durable,
                Keywords = new string[] { "image" },
                Description = "Nice image",
                FormattedPrice = "1.0",
                Tag = string.Empty
            };

            MockIAP.AddProductListing("img.3", p);

            p = new ProductListing
            {
                Name = "img.4",
                ImageUri = new Uri("/Res/Image/4.png", UriKind.Relative),
                ProductId = "img.4",
                ProductType = Windows.ApplicationModel.Store.ProductType.Durable,
                Keywords = new string[] { "image" },
                Description = "Nice image",
                FormattedPrice = "1.0",
                Tag = string.Empty
            };

            MockIAP.AddProductListing("img.4", p);

            p = new ProductListing
            {
                Name = "img.5",
                ImageUri = new Uri("/Res/Image/5.png", UriKind.Relative),
                ProductId = "img.5",
                ProductType = Windows.ApplicationModel.Store.ProductType.Durable,
                Keywords = new string[] { "image" },
                Description = "Nice image",
                FormattedPrice = "1.0",
                Tag = string.Empty
            };

            MockIAP.AddProductListing("img.5", p);

            p = new ProductListing
            {
                Name = "img.6",
                ImageUri = new Uri("/Res/Image/6.png", UriKind.Relative),
                ProductId = "img.6",
                ProductType = Windows.ApplicationModel.Store.ProductType.Durable,
                Keywords = new string[] { "image" },
                Description = "Nice image",
                FormattedPrice = "1.0",
                Tag = string.Empty
            };

            MockIAP.AddProductListing("img.6", p);
#endif
        }


        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Handle reset requests for clearing the backstack
            RootFrame.Navigated += CheckForResetNavigation;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            // If the app has received a 'reset' navigation, then we need to check
            // on the next navigation to see if the page stack should be reset
            if (e.NavigationMode == NavigationMode.Reset)
                RootFrame.Navigated += ClearBackStackAfterReset;
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            // Unregister the event so it doesn't get called again
            RootFrame.Navigated -= ClearBackStackAfterReset;

            // Only clear the stack for 'new' (forward) navigations
            if (e.NavigationMode != NavigationMode.New)
                return;

            // For UI consistency, clear the entire page stack
            while (RootFrame.RemoveBackEntry() != null)
            {
                ; // do nothing
            }
        }

        #endregion
    }
}