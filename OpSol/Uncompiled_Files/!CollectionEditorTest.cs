using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CustomControlExample
{
    // Custom class with multiple string properties
    public class MultiString
    {
        public string? String1 { get; set; } = "Default2";
        public string? String2 { get; set; } = "Default2";

        public override string ToString()
        {
            return $"{String1} - {String2}";
        }
    }

    [DesignerCategory("Code")]
    public class CustomStringControl : Button
    {
        [Category(".")]
        [Editor(typeof(SimpleMultiStringListEditor), typeof(UITypeEditor))]
        //[Editor("System.ComponentModel.Design.CollectionEditor, System.Design", typeof(UITypeEditor))]
        public List<MultiString> MyStrings { get; set; } = new List<MultiString>();
    }

    // Custom Collection Editor for List<MultiString>
    public class SimpleMultiStringListEditor : CollectionEditor
    {
        public SimpleMultiStringListEditor(Type type) : base(type) { }

        protected override CollectionForm CreateCollectionForm()
        {
            // Customize the collection form's appearance
            CollectionForm form = base.CreateCollectionForm();
            form.Text = "Edit List of MultiString Objects";
            form.BackColor = Color.Black;
            return form;
        }

        protected override object CreateInstance(Type itemType)
        {
            // Return a new MultiString object instead of a string
            if (itemType == typeof(MultiString))
            {
                return new MultiString();
            }
            return base.CreateInstance(itemType);
        }

        protected override string GetDisplayText(object value)
        {
            // Display each MultiString object as "String1 - String2"
            if (value is MultiString multiString)
            {
                return multiString.ToString() + "Hello there";
            }
            return base.GetDisplayText(value);
        }
    }
}
