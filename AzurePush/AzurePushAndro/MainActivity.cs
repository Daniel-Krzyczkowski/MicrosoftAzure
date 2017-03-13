using Android.App;
using Android.Widget;
using Android.OS;
using Gcm.Client;
using Android.Util;

namespace AzurePushAndro
{
    [Activity(Label = "AzurePushAndro", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            RegisterWithGCM();
        }

        private void RegisterWithGCM()
        {
            // Check to ensure everything's set up right
            GcmClient.CheckDevice(this);
            GcmClient.CheckManifest(this);

            // Register for push notifications
            Log.Info("MainActivity", "Registering...");
            PushHandlerService.Context = this;
            GcmClient.Register(this, Constants.SenderID);
        }
    }
}

