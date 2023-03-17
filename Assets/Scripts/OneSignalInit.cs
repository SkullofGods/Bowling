using System;
using System.Collections.Generic;
using OneSignalSDK;
using UnityEngine;
using UnityEngine.UI;

public class OneSignalInit : MonoBehaviour
{
    public string appId;
    void Start()
    {
        OneSignal.Default.Initialize(appId);
    }

    public async void PromptForPush() {
        var result = await OneSignal.Default.PromptForPushNotificationsWithUserResponse();
    }

    public void ClearPush() {
        OneSignal.Default.ClearOneSignalNotifications();
    }

    public async void SendPushToSelf() {
        var pushId = OneSignal.Default.PushSubscriptionState.userId;
        var pushOptions = new Dictionary<string, object> {
            ["contents"] = new Dictionary<string, string> {
                ["en"] = "Test Message"
            },

            // Send notification to this user
            ["include_player_ids"] = new List<string> { pushId },

            // Example of scheduling a notification in the future
            ["send_after"] = DateTime.Now.ToUniversalTime().AddSeconds(30).ToString("U")
        };

        var result = await OneSignal.Default.PostNotification(pushOptions);
    }
}
