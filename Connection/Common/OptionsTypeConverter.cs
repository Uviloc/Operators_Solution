using OperatorsSolution.Controls;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using static System.ComponentModel.TypeConverter;
using static OperatorsSolution.Controls.Logic_Button;
using Console = System.Diagnostics.Debug;
using static System.Windows.Forms.Design.AxImporter;

namespace OperatorsSolution.Common
{
    public class DynamicControlTypeConverter<T> : System.ComponentModel.TypeConverter where T : Control
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext? context) => true;


        public override bool GetStandardValuesExclusive(ITypeDescriptorContext? context) => true;


        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext? context)
        {
            return GetOptions(context);
        }


        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            // Allow conversion from string for objects that can be represented as strings
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }


        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is string str)
            {
                // Convert string back to an object of type T
                var options = GetOptions(context);
                return options.Cast<T>().FirstOrDefault(opt => opt?.ToString() == str)
                    ?? throw new NotSupportedException($"Cannot convert '{str}' to an instance of {typeof(T).Name}.");
            }

            return base.ConvertFrom(context, culture, value);
        }


        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is Control control)
            {
                // Convert the object (Control) to a string for display in the dropdown
                return control.Text; // Use the Text property for display
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        
        private static StandardValuesCollection GetOptions(ITypeDescriptorContext? context)
        {
            if (context?.Instance is Condition condition)
            {
                var parent = condition.parent; // Assuming this is a reference to the parent control/container
                if (parent != null)
                {
                    // Collect all children of type T within the parent
                    var availableControls = parent.Controls.OfType<T>().ToList();
                    return new StandardValuesCollection(availableControls);
                }
            }

            return new StandardValuesCollection(Array.Empty<T>());
        }
    }
}