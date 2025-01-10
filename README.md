This project is unfinished and not ready for use!

Operators Solution (Working name) is a framework for creating forms and databases.
The framework is meant to be fully self-contained so that sharing is made easier.


To add forms to the project, create a new "Class Library" with .NET 8.0 or higher and set the Operators Solution dll file as a project reference.
This will allow you to make full use of all the custom controls, classes and libraries.

Any forms or databases can be put into the Modules folder of the main app, this will then show up when the application is opened.
The main form class needs to inherit the class "PluginBaseForm" and use the interface "IFormPlugin".
As for the class name, choose something unique. Due to the current nature of the framework, duplicate classes are not loaded to avoid conflicts.
The FormName can be anything you want and is used to be displayed in the main application.
The ApplicationSettings should be set to "Settings.Default" for the main application to gain access to the form settings.
Regarding these form settings, the Settings.Designer file should include this code to function:
```
[global::System.Configuration.UserScopedSettingAttribute()]
[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
[global::System.Configuration.DefaultSettingValueAttribute("")]
public GraphicsSoftwareInfo GraphicsSoftwareInfo {
    get {
        return ((GraphicsSoftwareInfo)(this["GraphicsSoftwareInfo"]));
    }
    set {
        this["GraphicsSoftwareInfo"] = value;
    }
}

[global::System.Configuration.UserScopedSettingAttribute()]
[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
[global::System.Configuration.DefaultSettingValueAttribute("")]
public string ProjectFile {
    get {
        return ((string)(this["ProjectFile"]));
    }
    set {
        this["ProjectFile"] = value;
    }
}
```

Some usefull things for debugging:
Set the bost-build event in the properties to copy the created file into the main program for easier testing
xcopy "$(TargetDir)[your classname here].dll" "$[your program folder here]\Modules\Interfaces\" /Y


Important links:
https://trello.com/b/RLYMRfPi/opsol (Scrum Board for tasks, permission is required)

For any inquiries, email them at:
Isaac@Allus.tv
Isaac.van.Beek.1@gmail.com
