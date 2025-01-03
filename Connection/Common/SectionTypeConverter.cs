using OperatorsSolution.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = System.Diagnostics.Debug;
using static OperatorsSolution.Controls.Logic_Button;
using System.Diagnostics;

namespace OperatorsSolution.Common
{
    // MAKE MORE GENERAL AND NOT HARDCODED TO BE USEFULL FOR ANY CONTROLS
    public class SectionTypeDescriptionProvider : TypeDescriptionProvider
    {
        public SectionTypeDescriptionProvider() : base(TypeDescriptor.GetProvider(typeof(Section))){}

        public override ICustomTypeDescriptor? GetTypeDescriptor(Type objectType, object? instance)
        {
            var parent = base.GetTypeDescriptor(objectType, instance);
            return instance is Section section
                ? new SectionTypeConverter(parent, section)
                : parent;
        }
    }


    public class SectionTypeConverter(ICustomTypeDescriptor? parent, Section section) : CustomTypeDescriptor(parent)
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
            // Check if property has the ButtonTypeVisibilityAttribute
            var attribute = property.Attributes
                .OfType<ButtonTypeVisibilityAttribute>()
                .FirstOrDefault();

            if (attribute == null)
            {
                // Always show properties without the attribute
                return true;
            }

            // Show property if its ButtonType matches the section's ButtonType
            return attribute.ButtonType == section.ButtonType;
        }
    }
}
