using System.Configuration;

public class CustomSettingsProvider : SettingsProvider
{
    private readonly string _settingsFilePath;

    public CustomSettingsProvider(string settingsFilePath)
    {
        _settingsFilePath = settingsFilePath;
    }

    // Override to set the application name
    public override string ApplicationName
    {
        get => "YourAppName";  // Replace with your app name
        set { }
    }

    // Get property values from the settings file
    public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection properties)
    {
        var propertyValues = new SettingsPropertyValueCollection();

        foreach (SettingsProperty property in properties)
        {
            var value = ReadSettingFromFile(property.Name);
            var propertyValue = new SettingsPropertyValue(property) { PropertyValue = value };
            propertyValues.Add(propertyValue);
        }

        return propertyValues;
    }

    // Save property values to the settings file
    public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection propertyValues)
    {
        foreach (SettingsPropertyValue propertyValue in propertyValues)
        {
            SaveSettingToFile(propertyValue.Property.Name, propertyValue.PropertyValue);
        }
    }

    // Read setting value from the file (implement the actual read logic here)
    private object ReadSettingFromFile(string key)
    {
        // Use your preferred method to load settings from your file
        // For this example, return a default value
        return "Default Value";  // Placeholder
    }

    // Save setting value to the file (implement the actual write logic here)
    private void SaveSettingToFile(string key, object value)
    {
        // Write the setting to your custom settings file
        // For example, use serialization or simple file IO operations
    }
}
