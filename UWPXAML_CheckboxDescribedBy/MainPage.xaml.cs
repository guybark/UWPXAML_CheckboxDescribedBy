using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Controls;

namespace UWPXAML_CheckboxDescribedBy
{
    public sealed partial class MainPage : Page
    {
        private string demoAppGuid = "D94F9557-8142-4B72-BD52-9E1200EF6224";

        public MainPage()
        {
            this.InitializeComponent();

            // Have the CheckBox described by a nearby TextBlock.
            var describedBy = AutomationProperties.GetDescribedBy(PreloadCheckBox);
            describedBy.Add(PreloadDescriptionTB);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            IncludeSaladCheckBox.IsEnabled = (comboBox.SelectedIndex != 1);

            if (comboBox.SelectedIndex == 1)
            {
                if ((bool)IncludeSaladCheckBox.IsChecked)
                {
                    IncludeSaladCheckBox.IsChecked = false;

                    var peer = FrameworkElementAutomationPeer.FromElement(
                                IncludeSaladCheckBox);
                    if (peer != null)
                    {
                        // Any arbitrary string can be raised through this event.
                        var resourceLoader = ResourceLoader.GetForCurrentView();
                        var demoNotification = resourceLoader.GetString("DemoNotification");

                        // Raise a UIA Notification event. The specific AutomationNotificationKind and
                        // AutomationNotificationProcessing values used here are purely for demo purposes.
                        // A real app would use whatever values are most appropriate for their scenarios.
                        peer.RaiseNotificationEvent(
                             AutomationNotificationKind.ActionCompleted,
                             AutomationNotificationProcessing.All,
                             demoNotification,
                             demoAppGuid);
                    }
                }
            }
        }
    }
}
