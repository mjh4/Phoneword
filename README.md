Phoneword ReadMe
----------------

Phoneword is a sample Xamarin App that demonstrates Xamarin Studio’s ability to re-use code to efficiently create multi-platform solutions. 

Phoneword takes an alphanumeric input (i.e. 1-855-XAMARIN) and translates it to the corresponding telephone number (1-855-9262746). Once a number is translated, the user has the ability to call that number. Additionally, a record of phone calls made from the app are kept, and can be seen in a table in another view. 


Directories and File Info
-------------------------

### Phoneword ###

-PhoneTranslator.cs – This code defines the PhonewordTranslator class, which maps an alphanumeric input to the corresponding telephone number. This class is shared by Phoneworld_iOS and Phoneworld_Droid.

### Phoneword_Droid ####

This directory holds the Android specific files for the Phoneword app. This includes the AndroidManifest.xml file as well as the layout in the Main.axml file. Additionally, the two activities are defined in MainActivity.cs and CallHistoryActivity.cs. The MainActivity acts as the intermediary between the user and the PhoneTranslator code, and the CallHistoryActivity 

### Phoneword_iOS ###

This directory holds the iOS specific files for the Phoneword app. The layout is specified in the MainStoryboard.storyboard file. The view controllers for the two views are created in Phoneword_iOSViewController.cs and CallHistoryController.cs. The Phoneword view controller calls the code from PhoneTranslator.cs and handles user interaction. The CallHistoryController is a simple UITableView that is passed the list of called phone numbers by the Phoneword view controller. 


