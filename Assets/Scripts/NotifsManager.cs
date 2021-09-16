using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class NotifsManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        BuildNotificationChannel();
        BuildRepeatNotificationChannel()
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BuildNotificationChannel()
    {
        string channel_id = "default";
        string title = "Default Channel";
        string description = "Default Channel for this game";
        Importance importance = Importance.Default;
        

        AndroidNotificationChannel defaultChannel = new AndroidNotificationChannel(channel_id, title, description, importance);
        AndroidNotificationCenter.RegisterNotificationChannel(defaultChannel);
    }

    private void BuildRepeatNotificationChannel()
    {
        string channel_id = "repeat";
        string title = "Repeat Channel";
        string description = "Channel for repeating notifs";
        Importance importance = Importance.Default;


        AndroidNotificationChannel repeatChannel = new AndroidNotificationChannel(channel_id, title, description, importance);
        AndroidNotificationCenter.RegisterNotificationChannel(repeatChannel);
    }

    public void SendSimpleNotif()
    {
        string notif_title = "Simple notif";
        string notif_message = "This is a simple notif";
        System.DateTime fireTime = System.DateTime.Now.AddSeconds(10);

        AndroidNotification notif = new AndroidNotification(notif_title, notif_message, fireTime);

        AndroidNotificationCenter.SendNotification(notif, "default");
    }

    public void SendRepeatNotif()
    {
        string notif_title = "Repeat notif";
        string notif_message = "This is a repeat notif";
        System.DateTime fireTime = System.DateTime.Now.AddSeconds(10);
        System.TimeSpan interval = new System.TimeSpan(0, 2, 0);

        AndroidNotification notif = new AndroidNotification(notif_title, notif_message, fireTime, interval);

        AndroidNotificationCenter.SendNotification(notif, "repeat");
    }
}
