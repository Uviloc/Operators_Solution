using OperatorsSolution.Controls;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using Console = System.Diagnostics.Debug;

namespace OperatorsSolution.Common
{
    public class OptionsTypeConverter<T> : System.ComponentModel.TypeConverter where T : Control
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


        //public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        //{
        //    if (value is not string str)
        //        return base.ConvertFrom(context, culture, value);

        //    // Convert string back to an object of type T
        //    var options = GetOptions(context);
        //    var selected = options.Cast<T>().FirstOrDefault(opt => opt?.ToString() == str);

        //    // Ensure a valid value is assigned to the property for serialization
        //    if (context?.PropertyDescriptor != null && selected != null)
        //        context.PropertyDescriptor.SetValue(context.Instance, selected);

        //    return selected ?? throw new NotSupportedException($"Cannot convert '{str}' to an instance of {typeof(T).Name}.");
        //}
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is not string str)
                return base.ConvertFrom(context, culture, value);

            // Get the list of options (child controls of type T)
            var options = GetOptions(context);

            // Find the control whose Text matches the string
            var selected = options.Cast<T>().FirstOrDefault(opt => opt?.Text == str);

            if (selected == null)
            {
                Console.WriteLine($"No matching control found for '{str}'.");
                throw new NotSupportedException($"Cannot convert '{str}' to an instance of {typeof(T).Name}.");
            }

            // Debugging: Log the selected control
            Console.WriteLine($"Selected Control: {selected.Text}");

            // Set the property directly to the control reference
            if (context?.PropertyDescriptor != null)
                context.PropertyDescriptor.SetValue(context.Instance, selected);

            // Return the selected control instance
            return selected;
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

        //private static StandardValuesCollection GetOptions(ITypeDescriptorContext? context)
        //{
        //    // Get the parent control, if it is not found return an empty array
        //    if (context?.Instance is null || FindParentControl(context.Instance) is not Control parentControl)
        //        return new StandardValuesCollection(Array.Empty<T>());

        //    // Collect all children of type T within the parent control
        //    var availableControls = parentControl.Controls.OfType<T>().ToList();
        //    return new StandardValuesCollection(availableControls);
        //}
        private static StandardValuesCollection GetOptions(ITypeDescriptorContext? context)
        {
            if (context?.Instance is null || FindParentControl(context.Instance) is not Control parentControl)
                return new StandardValuesCollection(Array.Empty<T>());

            // Get all child controls of type T
            var availableControls = parentControl.Controls.OfType<T>().ToList();

            return new StandardValuesCollection(availableControls);
        }


        private static Control? FindParentControl(object instance)
        {
            // Check if the instance is a control itself
            if (instance is Control control)
                return control;

            // If it's a nested object, check for properties that might link to a parent
            var parentProperty = instance.GetType().GetProperty("Parent")?.GetValue(instance)
                                 ?? instance.GetType().GetProperty("parent")?.GetValue(instance);

            if (parentProperty != null)
                return FindParentControl(parentProperty);

            // If no parent property is found, try to traverse up using fields (for nested classes)
            foreach (var field in instance.GetType().GetFields(System.Reflection.BindingFlags.NonPublic |
                                                               System.Reflection.BindingFlags.Instance))
            {
                if (field.FieldType == typeof(Control) || field.FieldType.IsSubclassOf(typeof(Control)))
                {
                    if (field.GetValue(instance) is Control parentControl)
                        return parentControl;
                }
                else
                {
                    var parent = field.GetValue(instance);
                    if (parent != null)
                        return FindParentControl(parent);
                }
            }

            // No parent control found
            return null;
        }
    }
}