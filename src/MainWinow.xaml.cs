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
using System.CodeDom;
using System.Collections;
using System.Xml;

namespace Sandboxer
{

    public partial class MainWindow : FluentWindow
    {
        private string tempScriptPath = Path.Combine(Path.GetTempPath(), "sandboxer");
        private string localScriptPath = Path.Combine(Environment.CurrentDirectory, "scripts");

        public void initializeDynamicUI()
        {
            
            string[] tempFiles = Directory.GetFiles(tempScriptPath);
            string[] localFiles = Directory.Exists(localScriptPath) ? Directory.GetFiles(localScriptPath) : Array.Empty<string>();
            string[] allFiles = tempFiles.Concat(localFiles).ToArray();

            foreach (string file in allFiles)
            {
                createDynamicUI(file);
            }

            

        }

        public void createDynamicUI(string file)
        {
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

            cardControl.Content = DynamicArguments(file);

            StackPanelParent.Children.Add(grid);
            grid.Children.Add(cardControl);

            Separator separator = new Separator
            {
                Opacity = 0,
                Height = 10
            };

            StackPanelParent.Children.Add(separator);
        }

        private StackPanel DynamicArguments(string file)
        {
            string baseFileName = Path.GetFileNameWithoutExtension(file).Replace(" ", "");
            XmlNode attributeNode = PSParser.ParseXML(file).SelectSingleNode("/attributes");


            StackPanel sp = new StackPanel
            {
                Orientation = Orientation.Horizontal,
            };

            if (attributeNode != null){
                foreach (XmlNode childNode in attributeNode.ChildNodes)
                {
                    switch (childNode.Name)
                    {
                        case "checkbox":
                            createCheckBox(sp, childNode, baseFileName);
                            break;
                        default:
                            break;
                    }
                }
            }

            Wpf.Ui.Controls.Button button = new Wpf.Ui.Controls.Button
            {
                Content = "Execute",
                Appearance = Wpf.Ui.Controls.ControlAppearance.Primary
            };
            button.Click += (sender, e) =>
            {
                ExecutePowerShellScript(sp, file);
                button.Appearance = Wpf.Ui.Controls.ControlAppearance.Secondary;
            };
            sp.Children.Add(button);

            return sp;

        }

        private void createCheckBox(StackPanel sp, XmlNode node, string baseFileName)
        {
            string _checked = node.Attributes["checked"]?.Value;
            bool isChecked = false;
            if (_checked == null || bool.TryParse(_checked, out isChecked) == false) return;

            string uniquePSArgName = node.Attributes["uniquePSArgName"]?.Value;
            if (uniquePSArgName == null) return;

            string value = node.InnerText;
            if (value == null || value == "") return;

            CheckBox checkBox = new CheckBox
            {
                Content = value,
                Name = baseFileName + "_" + uniquePSArgName,
                IsChecked = isChecked
            };

            sp.Children.Add(checkBox);
            RegisterName(checkBox.Name, checkBox);
        }

        private void ExecutePowerShellScript(StackPanel parent, string scriptPath)
        {
            string dynamicArgumentString = "";
            string baseFileName = Path.GetFileNameWithoutExtension(scriptPath).Replace(" ", "");


            for (int i = 0; i < parent.Children.Count; i++)
            {
                // Skip the last child in the loop
                if (i == parent.Children.Count - 1)
                {
                    continue; // Skip this iteration
                }

                UIElement child = parent.Children[i];
                if (child is CheckBox checkbox)
                {
                    dynamicArgumentString += "-"+checkbox.Name.Split('_')[1] + " \"" + checkbox.IsChecked.ToString() + "\" ";
                }
            }

            

            try
            {
                // Create a new PowerShell process
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-ExecutionPolicy Bypass -File \"{scriptPath}\" {dynamicArgumentString}",
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