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

using System;
using System.IO;
using System.Reflection;

namespace Sandboxer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            System.IO.Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), "sandboxer"));
            Console.WriteLine("Extracting resources...");
            ExtractResource("ps1");
            Console.WriteLine("Starting application...");
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

        public static void ExtractResource(string embeddedFileName)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var arrResources = currentAssembly.GetManifestResourceNames();
            string tempScriptPath = Path.Combine(Path.GetTempPath(), "sandboxer");

            foreach (var resourceName in arrResources)
            {
                if (resourceName.ToUpper().EndsWith("." + embeddedFileName.ToUpper()))
                {
                    using (var resourceToSave = currentAssembly.GetManifestResourceStream(resourceName))
                    {

                        if (resourceToSave != null)
                        {
                            string originalName = GetOriginalFileName(resourceName);
                            string tempFilePath = Path.Combine(tempScriptPath, originalName);
                            using (var output = File.OpenWrite(tempFilePath))
                            {
                                resourceToSave.CopyTo(output);
                            }
                        }
                    }
                }
            }
        }

        public static string GetOriginalFileName(string resourceName)
        {
            string[] parts = resourceName.Split('.');
            if (parts.Length >= 3)
            {
                return string.Join(".", parts.Skip(2));
            }
            return resourceName;
        }
    }
}