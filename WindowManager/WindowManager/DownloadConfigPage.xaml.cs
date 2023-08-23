using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WindowManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DownloadConfigPage : Page
    {
        public DownloadConfigPage()
        {
            this.InitializeComponent();
            stackpanel.Height = MainWindow.settings.Grid.Height;
        }


        private async void PickAFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear previous returned file name, if it exists, between iterations of this scenario
            PickAFileOutputTextBlock.Text = "";

            // Create a file picker
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

            // Retrieve the window handle (HWND) of the current WinUI 3 window.
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);

            // Initialize the file picker with the window handle (HWND).
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            // Set options for your file picker
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.FileTypeFilter.Add(".json"); //filter for only json files

            // Open the picker for the user to pick a file
            var file = await openPicker.PickSingleFileAsync();

            //cleaner implementation
            if (file == null)
            {
                PickAFileOutputTextBlock.Text = "Operation cancelled.";
                return;
            }

            //PickAFileOutputTextBlock.Text = "Picked file: " + file.Name;
            
            string searchDirectory = Directory.GetCurrentDirectory();
            string settingsFilePath = FindSettingsFile(searchDirectory);
            string settingsFolder = Path.GetDirectoryName(settingsFilePath);
            try
            {
                // Move the selected file to the app's installation folder
               StorageFolder destinationFolder = await StorageFolder.GetFolderFromPathAsync(settingsFolder);
                // Move the selected file to the destination folder
               StorageFile settings = await StorageFile.GetFileFromPathAsync(settingsFilePath);

                // reading json content from original settings and storing panel number
                string jsonContent = await FileIO.ReadTextAsync(settings);
                SettingsData original_settings = JsonSerializer.Deserialize<SettingsData>(jsonContent);

                int tvPanelNumber = original_settings.Tv.PanelNum;

                // reading json content from new settings 
                string jsonContent_new = await FileIO.ReadTextAsync(file);
                SettingsData new_settings = JsonSerializer.Deserialize<SettingsData>(jsonContent_new);

                int new_panel_number = new_settings.Tv.PanelNum;

                //checking if layouts are compatible
                if (tvPanelNumber != new_panel_number)
                {
                    PickAFileOutputTextBlock.Text = "Incompatible app calibration."; //changed from "Incompatible file format."
                    return;
                }

                //establish minimum panel dimensions
                List<dynamic[]> new_panels = new List<dynamic[]>
                {
                    new dynamic[] { new_settings.Panels.Panel1?.ColumnSpan, new_settings.Panels.Panel1?.RowSpan, new_settings.Panels.Panel1?.PanelNum },
                    new dynamic[] { new_settings.Panels.Panel2?.ColumnSpan, new_settings.Panels.Panel2?.RowSpan, new_settings.Panels.Panel2?.PanelNum },
                    new dynamic[] { new_settings.Panels.Panel3?.ColumnSpan, new_settings.Panels.Panel3?.RowSpan, new_settings.Panels.Panel3?.PanelNum },
                    new dynamic[] { new_settings.Panels.Panel4?.ColumnSpan, new_settings.Panels.Panel4?.RowSpan, new_settings.Panels.Panel4?.PanelNum },
                    new dynamic[] { new_settings.Panels.Panel5?.ColumnSpan, new_settings.Panels.Panel5?.RowSpan, new_settings.Panels.Panel5?.PanelNum },
                    new dynamic[] { new_settings.Panels.Panel6?.ColumnSpan, new_settings.Panels.Panel6?.RowSpan, new_settings.Panels.Panel6?.PanelNum },
                    new dynamic[] { new_settings.Panels.Panel7?.ColumnSpan, new_settings.Panels.Panel7?.RowSpan, new_settings.Panels.Panel7?.PanelNum },
                    new dynamic[] { new_settings.Panels.Panel8?.ColumnSpan, new_settings.Panels.Panel8?.RowSpan, new_settings.Panels.Panel8?.PanelNum },
                    new dynamic[] { new_settings.Panels.Panel9?.ColumnSpan, new_settings.Panels.Panel9?.RowSpan, new_settings.Panels.Panel9?.PanelNum }
                };

                foreach (dynamic[] panel in new_panels)
                {
                    if (Array.TrueForAll(panel, item => item == null)) continue; //to fix fucky json -> c# nonsense

                    int a = (panel[2] - 1) % 3; //PanelNum to 0 index column position
                    int c = panel[0]; //ColumnSpan
                    double csize = 0;
                    for (int i = 0; i < c; i++) csize += original_settings.Grid.ColumnWidths[a + i];

                    int b = (int)Math.Floor((double)(panel[2] - 1) / 3); //PanelNum to 0 index row position
                    int r = panel[1]; //RowSpan
                    double rsize = 0;
                    for (int j = 0; j < r; j++) rsize += original_settings.Grid.RowHeights[b + j];

                    //check incoming format against minimum panel dimensions
                    if (rsize < MinimumDimensions.MinimumPanelHeight || csize < MinimumDimensions.MinimumPanelWidth)
                    {
                        PickAFileOutputTextBlock.Text = "Incompatible app calibration."; //changed from "Incompatible file format."
                        return;
                    }
                }

                //remove indent
                original_settings.Panels = new_settings.Panels;
                string updatedJsonContent = JsonSerializer.Serialize(original_settings, new JsonSerializerOptions { WriteIndented = true });

                // Overwrite the old settings file with the updated content
                await FileIO.WriteTextAsync(settings, updatedJsonContent);

                CalibrationWindow calibrationWindow = new CalibrationWindow();
                calibrationWindow.CalculateGridDimensions();

                //recalculate intermediate rectangles and optimal frames
                int screenPanel = MainWindow.settings.Tv.PanelNum;
                double[] ColumnWidths = MainWindow.settings.Grid.ColumnWidths;
                double[] RowHeights = MainWindow.settings.Grid.RowHeights;

                OptimalFrameMembers.intermediateRectangles = PanelAlgorithms.IntermediateRectangles(screenPanel, ColumnWidths, RowHeights, MinimumDimensions.MinimumPanelHeight, MinimumDimensions.MinimumPanelWidth);
                OptimalFrameMembers.optimalFrames = PanelAlgorithms.OptimalFrames(OptimalFrameMembers.intermediateRectangles);

                WindowManagerPage windowManagerPage = new WindowManagerPage();
                windowManagerPage.DisplayPanelsFromJSON(MainWindow.settings);

                //await file.CopyAndReplaceAsync(settings);
                PickAFileOutputTextBlock.Text = "Setting updated successfully.";
              
            }
            catch (Exception ex)
            {
                PickAFileOutputTextBlock.Text += "Error moving file: " + ex.Message;
            }
        }

        private async void ShareAFileButton_Click(object sender, RoutedEventArgs e){

           

            var folderPicker = new Windows.Storage.Pickers.FolderPicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);

            // Initialize the file picker with the window handle (HWND).
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hWnd);

            folderPicker.ViewMode = PickerViewMode.Thumbnail;

            // Open the picker for the user to pick a file
            var folder = await folderPicker.PickSingleFolderAsync();

            if (folder == null)
            {
                ShareAFileOutputTextBlock.Text = "Operation cancelled.";
                return;
            }

            else
            {
                string searchDirectory = Directory.GetCurrentDirectory();
                string settingsFilePath = FindSettingsFile(searchDirectory);
                //string settingsFolder = Path.GetDirectoryName(settingsFilePath);
                Windows.Storage.StorageFile settings = await Windows.Storage.StorageFile.GetFileFromPathAsync(settingsFilePath);

                try
                {
                    // Get the selected folder's path
                    string destinationFolderPath = folder.Path;

                    // Copy and replace the file in the destination folder
                    await settings.CopyAndReplaceAsync(await folder.CreateFileAsync(settings.Name, CreationCollisionOption.ReplaceExisting));

                    ShareAFileOutputTextBlock.Text = "File downloaded successfully. Please check the folder: " + folder.Name;
                }
                catch (Exception ex)
                {
                    ShareAFileOutputTextBlock.Text = "An error occurred: " + ex.Message;
                }
            }

        }


        static string FindSettingsFile(string directory)
        {
            string settingsFileName = "settings.json";
            string[] matchingFiles = Directory.GetFiles(directory, settingsFileName, SearchOption.AllDirectories);

            return matchingFiles.FirstOrDefault(); // Returns the first matching file or null if not found
        }
    }
}
