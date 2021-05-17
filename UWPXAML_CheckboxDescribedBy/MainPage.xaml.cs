using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Controls;

namespace UWPXAML_CheckboxDescribedBy
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Have the CheckBox described by a nearby TextBlock.
            var describedBy = AutomationProperties.GetDescribedBy(PreloadCheckBox);
            describedBy.Add(PreloadDescriptionTB);

            var saladDescribedBy = AutomationProperties.GetDescribedBy(IncludeSaladCheckBox);
            saladDescribedBy.Add(IncludeSaladDescriptionTB);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            var saladAvailable = (comboBox.SelectedIndex != 1);

            // Set the state of a CheckBox and its description TextBlock 
            // based on the selection made at the ComboBox.
            IncludeSaladCheckBox.IsEnabled = saladAvailable;
            IncludeSaladDescriptionTB.Visibility = (saladAvailable ?
                Visibility.Collapsed : Visibility.Visible);

            // We may need to clear a CheckBox based on the ComboBox selection.
            if (!saladAvailable)
            {
                IncludeSaladCheckBox.IsChecked = false;

                // Raise an event to make screen readers aware of the change
                // in state of the CheckBox.
                var peer = FrameworkElementAutomationPeer.FromElement(IncludeSaladDescriptionTB);
                if (peer != null)
                {
                    peer.RaiseAutomationEvent(AutomationEvents.LiveRegionChanged);
                }
            }
        }
    }
}
