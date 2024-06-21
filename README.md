

# Sandboxer
A tool to speed up windows sandbox configuration.
<img align="right" width="250" height="250" src="https://raw.githubusercontent.com/slluxx/Sandboxer/main/screenshot.jpg">


Windows Sandbox restricts users to a single Logon command, which can be quite limiting. While batch or PowerShell scripts offer some flexibility, they quickly reach their limits. For instance, if you need to set up a C++ toolchain or install WinGet and PowerShell 7 -but not simultaneously- the process becomes cumbersome. The more tasks you add, the more complicated and messy it gets. Even with a mounted script folder, you still need to set the execution policy and make other adjustments that can be unnatural and tedious to edit.

### Meet Sandboxer

Sandboxer is a C# tool featuring a user-friendly GUI with several built-in PowerShell scripts. You can easily extend the list of available scripts by adding your own to the `.\scripts\` directory. All scripts run with "ExecutionPolicy Bypass," ensuring they execute seamlessly without affecting the broader system scope.


There are two versions available:

- Lightweight Executable: If .NET 8 is installed on your machine, you can use the compact 5MB version of Sandboxer. This version leverages the existing .NET runtime, ensuring minimal disk space usage and faster startup times.

- Self-contained Executable: If .NET 8 is not installed, you will need to use the self-contained "full" version of Sandboxer. This version includes all necessary runtime components, ensuring Sandboxer runs smoothly on any system, albeit with a larger file size.

These options provide flexibility, allowing you to choose the version that best suits your system's capabilities and your preferences.


## Building

To build Sandboxer, you'll need the .NET SDK 8. Follow these steps:

1. Clone the repository.
2. Navigate to the `.\src` directory.
3. Run `dotnet build` for a standard build or `dotnet build -c NNL` for the lightweight version. Alternatively, you can use the `dotnet publish` command for creating deployable packages.

Note: The `csproj` file might need some adjustments, and contributions via pull requests are welcome.


## Licenses

This project is licensed under the GNU General Public License v3.0. For more details, see the LICENSE file.

In addition, this project uses the WPF UI library, which is licensed under the MIT License. For more details on the WPF UI library license, see the LICENSE-MIT file.
The WPF UI library is © 2021-2024 Leszek Pomianowski and WPF UI Contributors. More information can be found at https://dev.lepo.co/.