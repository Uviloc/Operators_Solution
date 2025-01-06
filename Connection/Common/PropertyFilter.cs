using OperatorsSolution.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = System.Diagnostics.Debug;
//using static OperatorsSolution.Controls.Logic_Button;
using System.Diagnostics;
using System.Data.Entity;

namespace OperatorsSolution.Common
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class TypeVisibilityAttribute(object type) : Attribute
    {
        public object Type { get; } = type;
    }

    public class TypeDescriptionProvider<T>() : TypeDescriptionProvider(TypeDescriptor.GetProvider(typeof(T)))
    {
        public override ICustomTypeDescriptor? GetTypeDescriptor(Type objectType, object? instance)
        {
            var parent = base.GetTypeDescriptor(objectType, instance);
            return instance != null
                ? new TypeConverter(parent, instance)
                : parent;
        }
    }


    public class TypeConverter(ICustomTypeDescriptor? parent, object context) : CustomTypeDescriptor(parent)
    {
        public override PropertyDescriptorCollection GetProperties(Attribute[]? attributes)
        {
            var allProperties = base.GetProperties(attributes);

            // Filter properties based on ButtonType
            var filteredProperties = allProperties.Cast<PropertyDescriptor>()
                .Where(prop => ShouldDisplayProperty(prop))
                .ToArray();

            return new PropertyDescriptorCollection(filteredProperties);
        }

        private bool ShouldDisplayProperty(PropertyDescriptor property)
        {
            // Check if the property has the TypeVisibilityAttribute
            var attributes = property.Attributes.OfType<TypeVisibilityAttribute>().ToList();

            if (attributes.Count == 0)
            {
                // Always show properties without the attribute
                return true;
            }


            // Determine the runtime property name dynamically
            string? contextPropertyName = attributes.FirstOrDefault()?.Type.GetType().Name;
            
            if (contextPropertyName == null)
                return false;

            // Get the property value from the context
            var propertyValue = context.GetType()
                .GetProperty(contextPropertyName)?
                .GetValue(context);

            //return Equals(attribute.Type, propertyValue);

            // Check if any attribute matches the current context value
            return attributes.Any(attr => Equals(attr.Type, propertyValue));
        }
    }
}
