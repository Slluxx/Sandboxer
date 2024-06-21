/*
 * Copyright (c) Slluxx
 *
 * This program is free software; you can redistribute it and/or modify it
 * under the terms and conditions of the GNU General Public License,
 * version 2, as published by the Free Software Foundation.
 *
 * This program is distributed in the hope it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for
 * more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
 
using System.Windows;
using Wpf.Ui.Controls;
using System.Windows.Media;
using System.IO;
using System.Windows.Controls;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace Sandboxer
{
    
    public partial class MainWindow : FluentWindow
    {
        private string tempScriptPath = Path.Combine(Path.GetTempPath(), "sandboxer");
        private string localScriptPath = Path.Combine(Environment.CurrentDirectory, "scripts");

        public void initializeDynamicUI()
        {
            Console.WriteLine("Creating dynamic UI...");
            string[] tempFiles = Directory.GetFiles(tempScriptPath);
            string[] localFiles = Directory.Exists(localScriptPath) ? Directory.GetFiles(localScriptPath) : Array.Empty<string>();
            string[] allFiles = tempFiles.Concat(localFiles).ToArray();

            foreach (string file in allFiles)
            {
                Console.WriteLine(file);
                createDynamicUI(file);
            }

            Console.WriteLine("Dynamic UI created.");

        }

        public void createDynamicUI(string file){
            if (Path.GetExtension(file) != ".ps1") return;

             Grid grid = new Grid
                {
                    ColumnDefinitions = {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) }
                    }
                };

                CardControl cardControl = new CardControl();
                Grid.SetColumn(cardControl, 0);

                Wpf.Ui.Controls.TextBlock headerTextBlock = new Wpf.Ui.Controls.TextBlock
                {
                    Text = Path.GetFileNameWithoutExtension(file)
                };
                cardControl.Header = headerTextBlock;

                // Set the CardControl content
                Wpf.Ui.Controls.Button button = new Wpf.Ui.Controls.Button
                {
                    Content = "Execute",
                    Appearance=Wpf.Ui.Controls.ControlAppearance.Primary
                };

                button.Click += (sender, e) =>
                {
                    ExecutePowerShellScript(file);
                    button.Appearance = Wpf.Ui.Controls.ControlAppearance.Secondary;
                };

                cardControl.Content = button;

                StackPanelParent.Children.Add(grid);
                grid.Children.Add(cardControl);

                Separator separator = new Separator
                {
                    Opacity = 0,
                    Height = 10
                };
                
                StackPanelParent.Children.Add(separator);
        }

        private void ExecutePowerShellScript(string scriptPath)
        {
            try
            {
                // Create a new PowerShell process
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-ExecutionPolicy Bypass -File \"{scriptPath}\"",
                    UseShellExecute = true,
                    CreateNoWindow = false // Set this to false to show PowerShell window
                };

                // Start the process
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error executing PowerShell script: {ex.Message}");
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            initializeDynamicUI();


        }
        /*
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Your click event handling logic here
            System.Windows.MessageBox.Show("Button Clicked! Text: " + MyTextBox.Text);
        }*/
    }
}