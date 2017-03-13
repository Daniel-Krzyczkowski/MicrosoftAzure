using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;
using Android.Util;
using Gcm.Client;
using WindowsAzure.Messaging;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]

namespace AzurePushAndro
{
    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE },
        Categories = new[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
        Categories = new[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
        Categories = new[] { "@PACKAGE_NAME@" })]
    public class AzurePushBroadcastReceiver : GcmBroadcastReceiverBase<PushHandlerService>
    {
        public const string TAG = "NotificationHubSample-LOG";
        public static string[] SENDER_IDS = { Constants.SenderID };

    }
}
