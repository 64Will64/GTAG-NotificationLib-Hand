# GTAG-NotificationLib
Notification ui mod for gorilla tag, allowed to be used by anyone.
Based of off the notifications from the "LHAX" mod menu, which was also made by larsl2005.

This fork is basically just the normal mod but just placed on your left hand.
made by 64Will64

# HOW TO INSTALL:
Put "GTAG_NotificationLib.dll" in the "plugins" folder in the BepInEx folder.

# HOW TO USE:
Add "GTAG_NotificationLib.dll" as an assembly reference to your project.
Put "using GTAG_NotificationLib;" at the top of the project.
To send a notification, use "NotifiLib.SendNotification("Whatever you want here");"
To check the previously sent notification. use "NotifiLib.PreviousNotifi" (to prevent sending the same notification twice for example);
To clear all notifications use NotifiLib.ClearAllNotifications()
To clear a certain amount of notifications use NotifiLib.ClearPastNotifications(amount (int))

ALL GORILLA TAG MODS THAT USE NOTIFICATIONLIB WILL ALSO WORK WITH THIS FORK
