using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.ApplicationModel.DataTransfer;
using Microsoft.UI.Xaml.Media.Imaging;
using WindowManager.UserControls;
using Microsoft.UI;
using WinRT.Interop;
using Windows.UI.WindowManagement;
using System.Reflection.Metadata;
using AppWindow = Microsoft.UI.Windowing.AppWindow;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowManager
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            TestFrame.Frame_DragStarting += new TypedEventHandler<UIElement, DragStartingEventArgs>(PanelFrame_DragStarting);

            // wire event handler to vent bubbled up from PanelFrame class
            //panelFrame.Frame_DragStarting += new RoutedEventHandler(PanelFrame_DragStarting);
            //panelFrame.Frame_DragStarting += PanelFrame_DragStarting;
            //panelFrame.Frame_DragStarting += new TypedEventHandler<UIElement, DragStartingEventArgs>(PanelFrame_DragStarting);

            System.Diagnostics.Debug.WriteLine("Initialising");


            AppWindow _appWindow = GetAppWindowForCurrentWindow();
            //            _appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);

            //System.Diagnostics.Debug.WriteLine(_appWindow.ClientSize)

        }

        private void PanelFrame_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            Frame frame = sender as Frame;

            // if sender frame contains grid
            if (frame.Content != null & frame.Content.GetType() == typeof(Grid))
            {
                Grid grid = frame.Content as Grid;

                // if grid contains webView2 child
                if (grid.Children.Count > 0 & grid.Children[1].GetType() == typeof(WebView2))
                {
                    // set payload of DataPackage to WebLink
                    WebView2 webView = grid.Children[1] as WebView2;
                    args.Data.SetWebLink(webView.Source);

                }

            }
            else { System.Diagnostics.Debug.WriteLine("null"); }
        }

        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);

            return AppWindow.GetFromWindowId(myWndId);
        }

        // event handlers
        private void Frame_DragStarting(UIElement sender, DragStartingEventArgs args)
        {

            Frame frame = sender as Frame;

            // if sender frame contains grid
            if (frame.Content != null & frame.Content.GetType() == typeof(Grid)) 
            {
                Grid grid = frame.Content as Grid;

                // if grid contains webView2 child
                if (grid.Children.Count > 0 & grid.Children[1].GetType() == typeof(WebView2))
                {
                    // set payload of DataPackage to WebLink
                    WebView2 webView = grid.Children[1] as WebView2;
                    args.Data.SetWebLink(webView.Source);

                }

            }
            else { System.Diagnostics.Debug.WriteLine("null"); }

        }

        private void Frame_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;

        }

        private async void Frame_Drop(object sender, DragEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Drop event handler triggered");

            Frame frame2 = sender as Frame;

            if (e.DataView.Contains(StandardDataFormats.WebLink))
            {
                var webLink = await e.DataView.GetWebLinkAsync();
                if (webLink != null)
                {

                    // swap content
                    frame2.Content = new WebView2 { Source = webLink };

                    // Set margin of webView (for now)
                    WebView2 webView = frame2.Content as WebView2;
                    webView.Margin = new Thickness(10);

                }
            }
        }

        private void Frame_DropCompleted(UIElement sender, DropCompletedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("DropCompleted event handler triggered");
            
            // remove frame1 content
            Frame originalFrame = sender as Frame;
            originalFrame.Content = null;

        }

        private void Frame_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Pointer entered");

            Frame frame = sender as Frame;
            Grid grid = frame.Content as Grid;

            if (grid.Children.Count > 0 & grid.Children[0].GetType() == typeof(RelativePanel))
            {
                RelativePanel relativePanel = grid.Children[0] as RelativePanel;
                
                if (relativePanel.Children.Count > 0)
                {
                    foreach (var child in relativePanel.Children)
                    {
                        if (child.GetType() == typeof(CommandBar))
                        {
                            child.Visibility = Visibility.Visible;
                        }
                    }
                }
            }

        }

        private void Frame_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Pointer exited");

            Frame frame = sender as Frame;
            Grid grid = frame.Content as Grid;

            if (grid.Children.Count > 0 & grid.Children[0].GetType() == typeof(RelativePanel))
            {
                RelativePanel relativePanel = grid.Children[0] as RelativePanel;

                if (relativePanel.Children.Count > 0)
                {
                    foreach (var child in relativePanel.Children)
                    {
                        if (child.GetType() == typeof(CommandBar))
                        {
                            child.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void Calibration_Click(object sender, RoutedEventArgs e)
        {
            CalibrationWindow calibrationWindow = new CalibrationWindow();
            calibrationWindow.Activate();
        }
    }
}
