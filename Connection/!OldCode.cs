< !--Include XPression if it is found -->
	<Choose>
		<When Condition="Exists('$(XPressionFolder)') And Exists('$(XPToolsLibFolder)')">
			<!-- Use Wildcards to find DLL File -->
			<ItemGroup>
				<XPressionFile Include="$(XPressionFolder)\**\*.dll" />
				<XPToolsLibFile Include="$(XPToolsLibFolder)\**\*.dll" />
			</ItemGroup>

			<!-- Set file path into property to use later and define constant HAS_XPRESSION -->
			<PropertyGroup>
				<XPressionPath>@(XPressionFile)</XPressionPath>
				<XPToolsLibPath>@(XPToolsLibFile)</XPToolsLibPath>
				<DefineConstants>$(DefineConstants); HAS_XPRESSION </ DefineConstants >

            </ PropertyGroup >


            < !--Import assembly reference -->
			<ItemGroup>
				<Reference Include="xpression.net">
					<HintPath>$(XPressionPath)</HintPath>
				</Reference>
				<Reference Include="xptoolslib.net">
					<HintPath>$(XPToolsLibPath)</HintPath>
				</Reference>
			</ItemGroup>
		</When>
	</Choose>

	<!-- Ensure that the DLLs are copied to the output directory (e.g., bin\Release) -->
	<Target Name="CopyXPressionDLLs" AfterTargets="Build">
		<Copy SourceFiles="$(XPressionPath)" DestinationFolder="$(OutDir)" />
		<Copy SourceFiles="$(XPToolsLibPath)" DestinationFolder="$(OutDir)" />
	</Target>






    //public ModuleLoader()
    //{
    //    LoadModules();
    //}

//public ModuleLoader()
//{
//    if (TryGetParentForm(out Form? form) && form != null)
//    {
//        form.Shown += LoadModules;
//    }
//}

//// This method checks if the form is shown
//private bool TryGetParentForm(out Form? form)
//{
//    form = null;

//    // Check if the component is assigned to a container
//    if (this.Site == null) return false;

//    // Get the parent container of the component
//    var container = this.Site.Container;

//    // Check if the container is a Form
//    if (container is not Form parentForm) return false;

//    form = parentForm;
//    return true;
//}










#if HAS_XPRESSION
using System;
using System.Collections.Generic;
using System.Threading.Channels;
using XPression;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;


namespace OperatorsSolution
{
    internal class XP_Functions
    {
        #region >----------------- XPression play scene: ---------------------
        /// <summary>
        /// Plays out the scene in XPression when this project is open in XPression.
        /// </summary>
        /// <param name = "Scene_Name">The xpScene in which the clip is located in.</param>
        /// <param name = "SD_Name">The name of the scene director.</param>
        /// <param name = "SD_Clip">The name of the clip that needs to be played.</param>
        /// <param name = "channel">The channel on which the clip needs to play.</param>
        /// <param name="layer">The layer on which the clip needs to play.</param>
        /// <returns>true if succsesfull, false if the clip could not be found</returns>
        public static bool PlaySceneState(xpScene Scene_Name, string? SD_Name, string SD_Clip, string? track, int channel = 0, int layer = 0)
        {
            // Set to be a default of the same name as its scene if it was not filled in
            if (string.IsNullOrWhiteSpace(SD_Name) || SD_Name == "Same as [Scene]")
            {
                SD_Name = Scene_Name.ToString();
            }
            if (string.IsNullOrWhiteSpace(track))
            {
                track = "StateTrack";
            }

            if (!Scene_Name.GetSceneDirectorByName(SD_Name, out xpSceneDirector Scene_Director) ||
                !Scene_Director.GetTrackByName(track, out xpSceneDirectorTrack Scene_State_Track) ||
                !Scene_State_Track.GetClipByName(SD_Clip, out xpSceneDirectorClip State_Clip))
            { return false; }

            Scene_Director.PlayRange(State_Clip.Position, State_Clip.Position);
            Scene_Name.SetOnline(channel, layer);
            Scene_Director.PlayRange(State_Clip.Position, State_Clip.Duration + State_Clip.Position, true);
            return true;
        }
        #endregion

        #region >----------------- XPression get material: ---------------------
        /// <summary>
        /// Gives the material of the specified object in a scene, given that this project is open in XPression.
        /// </summary>
        /// <param name="objectName">The name of the object (in XPression) to find the material of.</param>
        /// <param name="scene">The scene in which this material needs to be searched.</param>
        /// <returns>The IxpBaseObject as the material.</returns>
        public static IxpBaseObject getMaterialFromScene(string objectName, xpScene scene)
        {
            scene.GetObjectByName(objectName, out xpBaseObject sceneObject);

            return sceneObject;
        }
        #endregion


        #region >----------------- XPression set object: ---------------------
        public static void SetAllSceneMaterials(xpScene scene, List<ObjectChange> objectChanges)
        {
            xpEngine xpEngine = new();
            if (objectChanges.Count == 0 || xpEngine == null)
            {
                return;
            }
            
            foreach (ObjectChange objectChange in objectChanges)
            {
                SetMaterial(objectChange, scene, xpEngine);
                //if (objectChange.SceneObject == null)
                //{
                //    return;
                //}

                //if (objectChange.ObjectType == ObjectType.Text)
                //{
                //    xpTextObject textObject = (xpTextObject)XP_Functions.getMaterialFromScene(objectChange.SceneObject, scene);
                //    textObject.Text = objectChange.SetTo;
                //}
                //else if (objectChange.ObjectType == ObjectType.Material)
                //{
                //    scene.GetObjectByName(objectChange.SceneObject, out xpBaseObject baseObject);
                //    xpQuadObject quad = (xpQuadObject)baseObject;

                //    if (xpEngine.GetMaterialByName(objectChange.SetTo, out xpMaterial material))
                //    {
                //        quad.SetMaterial(0, material);
                //    }
                //}


                //if (SceneGraphic.GetObjectByName(objectChange.SceneObject, out xpBaseObject baseObject))
                //{
                //    if (baseObject is xpQuadObject quadObject)
                //    {
                //        if (XPression.GetMaterialByName(objectChange.SetTo, out xpMaterial material))
                //        {
                //            quadObject.SetMaterial(0, material);
                //        }
                //    }
                //    else if (baseObject is xpTextObject textObject)
                //    {
                //        textObject = (xpTextObject)XP_Functions.getMaterialFromScene(objectChange.SceneObject, SceneGraphic);
                //        textObject.Text = objectChange.SetTo;
                //    }
                //}
            }
        }

        public static void SetMaterial(ObjectChange objectChange, xpScene scene, xpEngine xpEngine)
        {
            // Check if any values are null
            if (objectChange.SceneObject == null)
            {
                return;
            }

            // Check what type of object it is
            if (!scene.GetObjectByName(objectChange.SceneObject, out xpBaseObject baseObject))
            {
                // Exit in case no scene object has been found
                return;
            }
            
            switch (baseObject)
            {
                case xpTextObject:
                    //xpTextObject textObject = (xpTextObject)XP_Functions.getMaterialFromScene(objectChange.SceneObject, scene);
                    xpTextObject textObject = (xpTextObject)baseObject;
                    textObject.Text = objectChange.SetTo;
                    break;
                case xpQuadObject:
                    xpQuadObject quad = (xpQuadObject)baseObject;

                    if (xpEngine.GetMaterialByName(objectChange.SetTo, out xpMaterial material))
                    {
                        quad.SetMaterial(0, material);
                    }
                    break;
                default:
                    MessageBox.Show("object type: " + baseObject.TypeName + " cannot be handled.");
                    return;
            }


            //if (objectChange.ObjectType == ObjectType.Text)
            //{
            //    xpTextObject textObject = (xpTextObject)XP_Functions.getMaterialFromScene(objectChange.SceneObject, scene);
            //    textObject.Text = objectChange.SetTo;
            //}
            //else if (objectChange.ObjectType == ObjectType.Material)
            //{
            //    scene.GetObjectByName(objectChange.SceneObject, out xpBaseObject baseObject);
            //    xpQuadObject quad = (xpQuadObject)baseObject;

            //    if (xpEngine.GetMaterialByName(objectChange.SetTo, out xpMaterial material))
            //    {
            //        quad.SetMaterial(0, material);
            //    }
            //}
        }

        // EXAMPLE FOR SETTING TEXT
        //private void button5_Click_2(object sender, EventArgs e)
        //{
        //    if (btn_BlueLT8TKO.Text == "Show Lowerthird")
        //    {
        //        if (XPression.GetSceneByName("Lowerthird", out LowerThird, true))
        //        {
        //            xpTextObject Name = (xpTextObject)XP_Functions.getMaterialFromScene("Name", LowerThird);
        //            xpTextObject Function = (xpTextObject)XP_Functions.getMaterialFromScene("Function", LowerThird);

        //            Name.Text = FighterLeft[0].FullName;
        //            Function.Text = "Fighter";

        //            if (XP_Functions.PlaySceneState(LowerThird, "Lowerthird", "in", 0, 0))
        //            {
        //                btn_BlueLT8TKO.Text = "Hide Lowerthird";
        //            }
        //        }
        //    }
        //    else if (btn_BlueLT8TKO.Text == "Hide Lowerthird")
        //    {
        //        if (XP_Functions.PlaySceneState(LowerThird, "Lowerthird", "out", 0, 0))
        //        {
        //            LowerThird.SetOffline();
        //            btn_BlueLT8TKO.Text = "Show Lowerthird";
        //        }
        //    }
        //}
        #endregion


        // EXAMPLE FOR SETTING SETTING MATERIAL
        //else if (btn_LowerRed8TKO.Text == "Next FighterEntry" && check_Lower4.Checked && status< 4)
        //    {
        //        //Get Materials for Signature
        //        xpQuadObject Signature = new xpQuadObject();

        //LowerThirdRed.GetObjectByName("SignaturePicture", out baseObject);
        //        Signature = (xpQuadObject) baseObject;

        //xpMaterial signatureMaterial = new xpMaterial();

        //        if (XPression.GetMaterialByName(FighterRight[0].SignatureMove, out signatureMaterial))
        //        {
        //            Signature.SetMaterial(0, signatureMaterial);
        //        }

        //        if (XP_Functions.PlaySceneState(LowerThirdRed, "FighterEntryRed", "out" + status.ToString(), 0, 0))
        //        {
        //            XP_Functions.PlaySceneState(LowerThirdRed, "FighterEntryRed", "in4", 0, 0);
        //            btn_LowerRed8TKO.Text = "Hide FighterEntry";
        //            status = 4;
        //        }
        //    }
    }
}
#endif

#region >----------------- Collection Classes: ---------------------
public class ClipPath()
{
    // Scene
    [Category("Search"),
    Description("Which scene this button will trigger.")]
    public string? Scene { get; set; }

    // Scene Director
    [Category("Search"),
    Description("(OPTIONAL) What scene director the clip is located in. Default: Same as [Scene]"),
    DefaultValue("Same as [Scene]")]
    public string? SceneDirector { get; set; } = "Same as [Scene]";

    // Clip
    [Category("Search"),
    Description("Which clip in this scene will trigger.")]
    public string? Clip { get; set; }

    // Track
    [Category("Search"),
    Description("(OPTIONAL) Which clip track the clip is in. Default: 'StateTrack'."),
    DefaultValue("StateTrack")]
    public string? Track { get; set; } = "StateTrack";



    // Channel
    [Category("Output"),
    Description("On what channel the clip will be displayed."),
    DefaultValue(0)]
    public int Channel { get; set; } = 0;

    // Layer
    [Category("Output"),
    Description("On what layer the clip will be displayed."),
    DefaultValue(0)]
    public int Layer { get; set; } = 0;





    // Object Changes
    [Category("Changes"),
    Description("Texts in the scene that need to be changed.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<ObjectChange> ObjectChanges { get; set; } = [];



    //// ButtonText
    //[Category(".Operation > Button"),
    //Description("(OPTIONAL) What text the button will change to. Default: 'Show + Same as next [Clip]'."),
    //DefaultValue("Show + Same as next [Clip]")]
    //public string? ButtonText { get; set; } = "Show + Same as next [Clip]";
}

//public enum ObjectType
//{
//    Text,
//    Material
//}

public class ObjectChange()
{
    [Category("Object Change")]
    public string? SceneObject { get; set; }

    // SET LATER TO SOMETHING FROM DATA MANAGER
    [Category("Object Change")]
    public string? SetTo { get; set; }

    //    [Category("Object Change")]
    //    [DefaultValue(ObjectType.Text)]
    //    public ObjectType ObjectType { get; set; } = ObjectType.Text;
}


// Done to dynamicly set the Scene to the previous Scene in the list
public class ClipPathCollection : Collection<ClipPath>
{
    protected override void InsertItem(int index, ClipPath item)
    {
        // If there are already items in the collection, set the new item's Scene to the last item's Scene
        if (this.Count > 0 && item.Scene == null)
        {
            item.Scene = this.Last().Scene;
        }

        base.InsertItem(index, item);
    }
}
    #endregion











    < !--Call the IncludeDll target for XPression -->
	<Target Name="IncludeXPression" DependsOnTargets="IncludeDLL">
		<PropertyGroup>
			<FolderPath>$(XPressionFolder)</FolderPath>
			<ReferenceName>xpression.net</ReferenceName>
		</PropertyGroup>
	</Target>

	<!-- Call the IncludeDll target for xpToolsLib -->
	<Target Name="IncludeXpToolsLib" DependsOnTargets="IncludeDLL">
		<PropertyGroup>
			<FolderPath>$(XpToolsLibFolder)</FolderPath>
			<ReferenceName>xpToolsLib.net</ReferenceName>
		</PropertyGroup>
	</Target>





    <Target Name="IncludeDLL" Condition="Exists('$(FolderPath)') And '$(RefernceName)' != ''">
		<!-- Use Wildcards to find DLL File -->
		<ItemGroup>
			<DllFile Include="$(FolderPath)\**\*.dll" />
		</ItemGroup>

		<!-- Set file path into property to use later and define constant HAS_ReferenceName -->
		<PropertyGroup>
			<DllPath>@(DllFile)</DllPath>
			<DefineConstants>$(DefineConstants); HAS_$(ReferenceName)</DefineConstants>
		</PropertyGroup>

		<!-- Import assembly reference -->
		<ItemGroup>
			<Reference Include="$(ReferenceName)">
				<HintPath>$(DllPath)</HintPath>
			</Reference>
		</ItemGroup>
	</Target>





    <!-- Include XPression if it is found -->
	<Choose>
		<When Condition="Exists('$(XPressionFolder)')">
			<!-- Use Wildcards to find DLL File -->
			<ItemGroup>
				<XPressionFile Include="$(XPressionFolder)\**\*.dll" />
			</ItemGroup>

			<!-- Set file path into property to use later and define constant HAS_XPRESSION -->
			<PropertyGroup>
				<XPressionPath>@(XPressionFile)</XPressionPath>
				<DefineConstants>$(DefineConstants); HAS_XPRESSION </ DefineConstants >

            </ PropertyGroup >


            < !--Import assembly reference -->
			<ItemGroup>
				<Reference Include="xpression.net">
					<HintPath>$(XPressionPath)</HintPath>
				</Reference>
			</ItemGroup>
		</When>
	</Choose>





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

// Condition="'$(DefineConstants)'=='HAS_XPRESSION'"



< PropertyGroup >

        < DefineConstants Condition = "Exists('..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpression.net\10.5.0.5508__9632b4b433765424\xpression.net.dll')

                                And Exists('..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpToolsLib.net\10.5.0.5508__28a9dbdb95a1c855\xptoolslib.net.dll')">
			$(DefineConstants); HAS_XPRESSION </ DefineConstants >

    </ PropertyGroup >


    < ItemGroup Condition = "Exists('..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpression.net\10.5.0.5508__9632b4b433765424\xpression.net.dll')" >

        < Reference Include = "xpression.net" >

            < HintPath > ..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpression.net\10.5.0.5508__9632b4b433765424\xpression.net.dll</HintPath>
		</Reference>
	</ItemGroup>

	<ItemGroup Condition="Exists('..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpToolsLib.net\10.5.0.5508__28a9dbdb95a1c855\xptoolslib.net.dll')">
		<Reference Include="xpToolsLib.net">
			<HintPath>..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpToolsLib.net\10.5.0.5508__28a9dbdb95a1c855\xptoolslib.net.dll</HintPath>
		</Reference>
	</ItemGroup>

	<PropertyGroup Condition="'$(XpressionNetPath)' != ''">
		<DefineConstants Condition="Exists($(XpressionNetPath))
								And Exists($(XpToolsLibPath))">
			$(DefineConstants); HAS_XPRESSION
        </ DefineConstants >

    </ PropertyGroup >

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



















    < Project Sdk = "Microsoft.NET.Sdk" >



    < PropertyGroup >

        < OutputType > WinExe </ OutputType >

        < TargetFramework > net8.0 - windows </ TargetFramework >

        < Nullable > enable </ Nullable >

        < UseWindowsForms > true </ UseWindowsForms >

        < ImplicitUsings > enable </ ImplicitUsings >


        < XpressionNetPath1 > ..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpression.net\10.5.0.5508__9632b4b433765424\xpression.net.dll</XpressionNetPath1>
		<!--<XpToolsLibPath1>..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpToolsLib.net\10.5.0.5508__28a9dbdb95a1c855\xptoolslib.net.dll</XpToolsLibPath>-->
	</PropertyGroup>


	<ItemGroup>
		<Compile Remove="!AssemblyLoader.cs" />
		<Compile Remove="OldCode.cs" />
	</ItemGroup>

	<ItemGroup>
		<None Include="!AssemblyLoader.cs" />
		<None Include="OldCode.cs" />
	</ItemGroup>

	<ItemGroup>
		<PackageReference Include="Dapper" Version="2.1.35" />
		<PackageReference Include="Emitter" Version="1.0.41" />
		<PackageReference Include="EntityFramework" Version="6.5.1" />
		<PackageReference Include="ExcelDataReader" Version="3.7.0" />
		<PackageReference Include="ExcelDataReader.DataSet" Version="3.7.0" />
		<PackageReference Include="Rug.Osc" Version="1.2.5" />
		<PackageReference Include="SQLite" Version="3.13.0" />
		<PackageReference Include="Stub.System.Data.SQLite.Core.NetFramework" Version="1.0.119" />
		<PackageReference Include="System.Data.SQLite" Version="1.0.119" />
		<PackageReference Include="System.Data.SQLite.Core" Version="1.0.119" />
		<PackageReference Include="System.Data.SQLite.EF6" Version="1.0.119" />
		<PackageReference Include="System.Data.SQLite.Linq" Version="1.0.119" />
	</ItemGroup>





	<!--Dynamicly checks for if the dll files of XPression exist and will load them if they do, and set the constant so that the code associated will be used-->
	<!-- Use Wildcards to Include DLL Files -->
	<ItemGroup>
		<XpressionNetFiles Include="..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpression.net\*\xpression.net.dll" />
		<XpToolsLibFiles Include="..\..\..\..\..\..\..\..\Windows\assembly\GAC_MSIL\xpToolsLib.net\*\xptoolslib.net.dll" />
	</ItemGroup>

	<!-- Set Properties Based on Included Files -->
	<PropertyGroup Condition="'$(XpressionNetFiles)' != '' And '$(XpToolsLibFiles)' != ''">
		<XpressionNetPath>@(XpressionNetFiles)</XpressionNetPath>
		<XpToolsLibPath>@(XpToolsLibFiles)</XpToolsLibPath>
	</PropertyGroup>


	<PropertyGroup Condition="'$(XpressionNetPath)' != ''">
		<DefineConstants>$(DefineConstants); HAS_XPRESSION </ DefineConstants >

    </ PropertyGroup >



    < ItemGroup Condition = "Exists($(XpressionNetPath))" >

        < Reference Include = "xpression.net" >

            < HintPath >$(XpressionNetPath) </ HintPath >

        </ Reference >

    </ ItemGroup >


    < ItemGroup Condition = "Exists($(XpToolsLibPath))" >

        < Reference Include = "xpToolsLib.net" >

            < HintPath >$(XpToolsLibPath) </ HintPath >

        </ Reference >

    </ ItemGroup >



    < Target Name = "Test" AfterTargets = "ResolveReferences" Condition = "$(MyVariable) != 'true'" >

        < Message Text = "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!|  @(XpressionNetFiles)  |!!!!!" Importance = "High" />

        < Message Text = "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!|  $(XpressionNetPath)  |!!!!!" Importance = "High" />

    </ Target >

</ Project >