using System;
using System.Globalization;
using Root.Utils;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Assertions;

// Recuerden que tienen que agregar el package de Mobile Notifications para que esto funcione.
public static class MobileNotificationManager
{
    private static AndroidNotificationChannel _notifChannel;
    private static AndroidNotificationChannel _staminaChannel;
    public const string NotifChannelID = "reminder_notif_ch";
    public const string StaminaChannelID = "stamina_notif_ch";
    private const string LogBackInNotificationTitleID = "ID_LogBackInNotificationTitle";
    private const string LogBackInNotificationDescriptionID = "ID_LogBackInNotificationDescription";
    private const string UpdateInNotificationTitleID = "ID_UpdateNotificationTitle";
    private const string UpdateInNotificationDescriptionID = "ID_UpdateNotificationDescription";
    private const string DateFormat = "dd MM yyyy HH mm";
    

    public static void Initialize()
    {
        Debug.Log("Initializing Notifications");
        // Limpieza preventiva inicial.
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        AndroidNotificationCenter.CancelAllScheduledNotifications();

        // De esta forma definitos TIPOS de notificaciones, pueden crear todos los que quieran.
        _staminaChannel = new AndroidNotificationChannel()
        {
            Id = StaminaChannelID,
            Name = "Stamina Notifications",
            Description = "Stamina refilled notifications",
            Importance = Importance.High         
        };
        
        _notifChannel = new AndroidNotificationChannel()
        {
            Id = NotifChannelID,
            Name = "Reminder Notifications",
            Description = "Reminders to login",
            Importance = Importance.High         
        };

        AndroidNotificationCenter.RegisterNotificationChannel(_notifChannel);
        AndroidNotificationCenter.RegisterNotificationChannel(_staminaChannel);

        var localization = Localization.Ins;
        Assert.IsTrue(localization.IsInitialized(), "Localization Service not yet initialized");
        var loginNotificationTitleString = localization.GetTranslate(LogBackInNotificationTitleID);
        var loginNotificationDescriptionString = localization.GetTranslate(LogBackInNotificationDescriptionID);
        DisplayNotification(NotifChannelID, loginNotificationTitleString, loginNotificationDescriptionString, IconSelecter.myicon_0, IconSelecter.myicon_1, DateTime.Now.AddMinutes(1));
        
        Assert.IsTrue(RemoteManager.IsInitialized, "Remote Manager not initialized");
        var updateDate = DateTime.ParseExact(RemoteManager.GetString("nextUpdateDate"), DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
        var updateNotificationTitleString = localization.GetTranslate(UpdateInNotificationTitleID);
        var updateNotificationDescriptionString = localization.GetTranslate(UpdateInNotificationDescriptionID);
        DisplayNotification(StaminaChannelID, updateNotificationTitleString, updateNotificationDescriptionString, IconSelecter.myicon_0, IconSelecter.myicon_1, updateDate);
        
        Debug.Log("Initialized Notifications");
    }

    public static int DisplayNotification(string notifChannelID, string title, string text, IconSelecter iconSmall, IconSelecter iconLarge, DateTime fireTime)
    {
        Debug.Log(notifChannelID + " " + title + " " + text);
        // Estas son realmente las notificaciones que ver� el usuario y que al hacer el request, quedan vinculadas a un channel creado previamente.
        AndroidNotification notification = new()
        {
            Title = title,
            Text = text,
            SmallIcon = iconSmall.ToString(),
            LargeIcon = iconLarge.ToString(),
            FireTime = fireTime
        };

        return AndroidNotificationCenter.SendNotification(notification, notifChannelID);
    }

    public static void CancelNotification(int id)
    {
        AndroidNotificationCenter.CancelScheduledNotification(id);
    }
}

public enum IconSelecter
{
    myicon_0,
    myicon_1
}
