using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;

namespace UWPXAML_CheckboxDescribedBy
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Have the CheckBox described by a nearby TextBlock.
            var describedBy = AutomationProperties.GetDescribedBy(PreloadCheckbox);
            describedBy.Add(PreloadDescription);
        }
    }
}
