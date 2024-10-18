//private void allButtonHandler_Click(object? sender, EventArgs e)
//{
//    if (sender is OperatorButton button)
//    {
//        //// Unassign allButtonHandler function, assign correct function and trigger immidiately.
//        //button.Click -= allButtonHandler_Click;
//        //button.Click += (s, args) => Btn_TriggerScene_Click(s, args);
//        //buttonHandlers.Btn_TriggerScene_Click(sender, e);
//    }
//}






//// Scene
//[
//    Category(".Operation > Search"),
//    //PropertyOrder(1),
//    Description("Which scene this button will trigger.")
//]
//public string? Scene { get; set; }

//// Scene Director
//[
//    Category(".Operation > Search"),
//    //PropertyOrder(2),
//    Description("(OPTIONAL) What scene director the clip is located in. Default: Same as [Scene]"),
//    DefaultValue("Same as [Scene]")
//]
//public string? SceneDirector { get; set; }

//// Clip
//[
//    Category(".Operation > Search"),
//    //PropertyOrder(3),
//    Description("Which clip in this scene will trigger.")
//]
//public string? Clip { get; set; }

//// Track
//[
//    Category(".Operation > Search"),
//    //PropertyOrder(3),
//    Description("(OPTIONAL) Which clip track the clip is in. Default: 'StateTrack'."),
//    DefaultValue("StateTrack")
//]
//public string? Track { get; set; }



//// Channel
//[
//    Category(".Operation > Output"),
//    //PropertyOrder(1),
//    Description("On what channel the clip will be displayed."),
//    DefaultValue(0)
//]
//public int Channel { get; set; }

//// Layer
//[
//    Category(".Operation > Output"),
//    //PropertyOrder(2),
//    Description("On what layer the clip will be displayed."),
//    DefaultValue(0)
//]
//public int Layer { get; set; }





// Inside CustomTools:


//private bool _showOtherAttribute;

//// Property to influence the visibility of another property
//[
//    Category(".Operation > General"),
//    Description("If true, BasedOnProperty1 will be visible.")
//]
//public bool ShowOtherAttribute
//{
//    get => _showOtherAttribute;
//    set
//    {
//        if (_showOtherAttribute != value) // Only update if the value has changed
//        {
//            _showOtherAttribute = value;
//            OnPropertyChanged(nameof(ShowOtherAttribute));
//            OnPropertyChanged(nameof(BasedOnProperty1)); // Notify that BasedOnProperty1 may have changed
//        }
//    }
//}

//// This property will be conditionally visible based on ShowOtherAttribute
//[Browsable(false)] // Hidden by default
//[Category(".Operation > General")]
//public string BasedOnProperty1
//{
//    get { return "This property is conditionally visible."; }
//}


//public event EventHandler<PropertyChangedEventArgs> PropertyChanged;

//protected virtual void OnPropertyChanged(string propertyName)
//{
//    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//}

//public protected override ICustomTypeDescriptor GetTypeDescriptor(Type type)
//{
//    return new OperatorButtonTypeDescriptor(TypeDescriptor.GetProvider(type).GetTypeDescriptor(type), this);
//}

//public void OperatorButton_PropertyChanged(object sender, PropertyChangedEventArgs e)
//{
//    if (e.PropertyName == nameof(ShowOtherAttribute))
//    {
//        // Notify that BasedOnProperty1 should change visibility
//        OnPropertyChanged(nameof(BasedOnProperty1));
//    }
//}

//public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
//{
//    // Call the base class to get the default property collection
//    var properties = TypeDescriptor.GetProperties(this, attributes);
//    var propertyArray = new PropertyDescriptor[properties.Count];

//    for (int i = 0; i < properties.Count; i++)
//    {
//        var property = properties[i];

//        // Modify the visibility of BasedOnProperty1
//        if (property.Name == nameof(BasedOnProperty1))
//        {
//            // Create a new BrowsablePropertyDescriptor to control visibility
//            propertyArray[i] = new BrowsablePropertyDescriptor(property, ShowOtherAttribute);
//        }
//        else
//        {
//            propertyArray[i] = property;
//        }
//    }

//    return new PropertyDescriptorCollection(propertyArray);
//}

////Custom Type Descriptor to manage visibility
//private class OperatorButtonTypeDescriptor : CustomTypeDescriptor
//{
//    private readonly OperatorButton _button;

//    public OperatorButtonTypeDescriptor(ICustomTypeDescriptor parent, OperatorButton button)
//        : base(parent)
//    {
//        _button = button;
//    }

//    public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
//    {
//        var properties = base.GetProperties(attributes);
//        var propertyArray = new PropertyDescriptor[properties.Count];

//        for (int i = 0; i < properties.Count; i++)
//        {
//            var property = properties[i];

//            // Modify the visibility of BasedOnProperty1
//            if (property.Name == nameof(OperatorButton.BasedOnProperty1))
//            {
//                // Set Browsable based on ShowOtherAttribute
//                propertyArray[i] = new BrowsablePropertyDescriptor(property, _button.ShowOtherAttribute);
//            }
//            else
//            {
//                propertyArray[i] = property;
//            }
//        }

//        return new PropertyDescriptorCollection(propertyArray);
//    }
//}

//// Custom Property Descriptor to conditionally show/hide properties
//private class BrowsablePropertyDescriptor : PropertyDescriptor
//{
//    private readonly PropertyDescriptor _baseProperty;
//    private readonly bool _isBrowsable;

//    public BrowsablePropertyDescriptor(PropertyDescriptor baseProperty, bool isBrowsable) : base(baseProperty)
//    {
//        _baseProperty = baseProperty;
//        _isBrowsable = isBrowsable;
//    }

//    public override bool CanResetValue(object component) => _baseProperty.CanResetValue(component);
//    public override object GetValue(object component) => _baseProperty.GetValue(component);
//    public override bool IsReadOnly => _baseProperty.IsReadOnly;
//    public override Type ComponentType => _baseProperty.ComponentType;
//    public override bool ShouldSerializeValue(object component) => _baseProperty.ShouldSerializeValue(component);
//    public override void ResetValue(object component) => _baseProperty.ResetValue(component);
//    public override void SetValue(object component, object value) => _baseProperty.SetValue(component, value);
//    public override bool IsBrowsable => _isBrowsable; // Control visibility here
//    public override Type PropertyType => _baseProperty.PropertyType;
//}