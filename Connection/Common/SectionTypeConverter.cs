using OperatorsSolution.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = System.Diagnostics.Debug;
using static OperatorsSolution.Controls.Logic_Button;

namespace OperatorsSolution.Common
{
    // MAKE MORE GENERAL AND NOT HARDCODED TO BE USEFULL FOR ANY CONTROLS
    public class SectionTypeDescriptionProvider : TypeDescriptionProvider
    {
        public SectionTypeDescriptionProvider() : base(TypeDescriptor.GetProvider(typeof(Section)))
        {
        }

        public override ICustomTypeDescriptor? GetTypeDescriptor(Type objectType, object? instance)
        {
            var parent = base.GetTypeDescriptor(objectType, instance);
            return instance is Section section
                ? new SectionTypeConverter(parent, section)
                : parent;
        }
    }


    public class SectionTypeConverter : CustomTypeDescriptor
    {
        private readonly Section _section;

        public SectionTypeConverter(ICustomTypeDescriptor? parent, Section section) : base(parent)
        {
            _section = section;
        }

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
            // Always show shared properties
            if (property.Name == nameof(Section.ButtonType))
            {
                return true;
            }

            // Show properties specific to ScriptButton
            if (_section.ButtonType == ButtonType.ScriptButton &&
                property.Name == nameof(Section.Scenes))
            {
                return true;
            }

            // Show properties specific to ToggleButton
            if (_section.ButtonType == ButtonType.ToggleButton &&
                (property.Name == nameof(Section.Text) || property.Name == nameof(Section.SceneName)))
            {
                return true;
            }

            return false;
        }
    }
}
