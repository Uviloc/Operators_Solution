using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Console = System.Diagnostics.Debug;


namespace OperatorsSolution.Common
{
    public class PluginSettingsProvider : SettingsProvider
    {
        private readonly string _pluginSettingsFilePath;

        public PluginSettingsProvider(string pluginSettingsFilePath)
        {
            _pluginSettingsFilePath = pluginSettingsFilePath;
        }

        public override string Name => "PluginSettingsProvider";
        public override string ApplicationName
        {
            get => AppDomain.CurrentDomain.FriendlyName; // Use the application name or plugin name
            set { } // Ignored
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Provider name cannot be null or empty.", nameof(name));
        }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection properties)
        {
            var propertyValues = new SettingsPropertyValueCollection();
            foreach (SettingsProperty property in properties)
            {
                var value = ReadSettingFromFile(property.Name, property.PropertyType);
                var propertyValue = new SettingsPropertyValue(property)
                {
                    IsDirty = false,
                    SerializedValue = value ?? property.DefaultValue
                };
                propertyValues.Add(propertyValue);
            }

            return propertyValues;
        }


        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection propertyValues)
        {
            foreach (SettingsPropertyValue propertyValue in propertyValues)
            {
                SaveSettingToFile(propertyValue.Name, propertyValue.SerializedValue?.ToString());
            }
        }

        private object? ReadSettingFromFile(string key, Type type)
        {
            if (!File.Exists(_pluginSettingsFilePath))
                return null;


            var text = File.ReadAllText(_pluginSettingsFilePath);

            //text.

            //foreach (var line in lines)
            //{
            //    //if (!line.StartsWith("<"))
            //    //{
            //    //    Type type = line.Split("=");
            //    //}



            //    var parts = line.Split(new[] { '=' }, 2, StringSplitOptions.None);
            //    if (parts.Length == 2 && parts[0].Trim() == key)
            //    {
            //        var value = parts[1].Trim();

            //        // If the type is a string, just return the value
            //        if (type == typeof(string))
            //        {
            //            return value;
            //        }

            //        // If the value looks like XML (check if it starts with '<'), assume it needs deserialization
            //        if (value.StartsWith("<"))
            //        {
            //            return DeserializeXml(value, type);
            //        }
            //    }
            //}

            return null;
        }

        // Deserialize XML into the specified type
        private object? DeserializeXml(string xmlContent, Type type)
        {
            var serializer = new XmlSerializer(type);
            using var reader = new StringReader(xmlContent);
            return serializer.Deserialize(reader);
        }


        private void SaveSettingToFile(string key, string? value)
        {
            var lines = File.Exists(_pluginSettingsFilePath)
                ? File.ReadAllLines(_pluginSettingsFilePath).ToList()
                : new List<string>();

            var existingIndex = lines.FindIndex(line => line.StartsWith(key + "="));
            if (existingIndex >= 0)
            {
                lines[existingIndex] = $"{key}={value}";
            }
            else
            {
                lines.Add($"{key}={value}");
            }

            File.WriteAllLines(_pluginSettingsFilePath, lines);
        }
    }
}