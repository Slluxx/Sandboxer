
# Sandboxer

Sandboxer is a C# tool designed to streamline Windows Sandbox configuration, offering a user-friendly GUI and several built-in PowerShell scripts. You can enhance its functionality by adding your own scripts.


<p align="center">
  <img align="center" width="700" height="auto" src="https://raw.githubusercontent.com/Slluxx/Sandboxer/main/screenshot.jpg">
</p>


## Why Sandboxer?

Windows Sandbox limits users to a single Logon command, which can be restrictive. While batch or PowerShell scripts provide flexibility, managing multiple tasks that you only sometimes want can become cumbersome. Sandboxer simplifies this process by allowing scripts to execute seamlessly with "ExecutionPolicy Bypass," ensuring they run without impacting system settings.

## Extending Functionality with Custom Scripts

Extend Sandboxer's capabilities by adding your own scripts. Scripts can introduce custom UI elements (currently checkboxes) defined in a specific format using XML-like syntax:

```xml
#! SANDBOXER: <attributes>
#! SANDBOXER:   <checkbox checked="True" uniquePSArgName="chromeInstall">Chrome</checkbox>
#! SANDBOXER:   <checkbox checked="False" uniquePSArgName="firefoxInstall">Firefox</checkbox>
#! SANDBOXER:   <checkbox checked="False" uniquePSArgName="edgeInstall">Edge</checkbox>
#! SANDBOXER: </attributes>
```

(For detailed usage, refer to the [Install Browser](https://github.com/Slluxx/Sandboxer/blob/main/src/Resources/Install%20Browser.ps1) PowerShell script.)

## Version Options

Choose from two versions based on your system's requirements:
- **Lightweight Executable**: Utilizes the existing .NET runtime (requires .NET 8), offering a compact 5MB version with faster startup times.
- **Self-contained Executable**: Includes all necessary runtime components, ensuring compatibility on systems without .NET 8 installed, albeit with a larger file size.

## Getting Started with Sandboxer

To use Sandboxer in Windows Sandbox:
1. Mount a folder containing Sandboxer.
2. Execute Sandboxer using the logon command.
   
For example, mount the host desktop into the sandbox and run Sandboxer from there. Alternatively, set `ReadOnly` to `true` to prevent downloaded files from interacting with the host PC.

```xml
<Configuration>
  <MappedFolders>
    <MappedFolder>
      <HostFolder>C:\Users\Username\Desktop</HostFolder>
      <SandboxFolder>C:\Users\WDAGUtilityAccount\Desktop\Host</SandboxFolder>
      <ReadOnly>false</ReadOnly>
    </MappedFolder>
  </MappedFolders>
  <LogonCommand>
    <Command>C:\Users\WDAGUtilityAccount\Desktop\Host\sandboxer\sandboxer.exe</Command>
  </LogonCommand>
</Configuration>
```

## Building Sandboxer

To build Sandboxer:
1. Clone the repository.
2. Navigate to the `.\src` directory.
3. Run `dotnet build` for a standard build or `dotnet build -c NNL` for the lightweight version. Alternatively, use `dotnet publish` to create deployable packages.

Adjustments to the `csproj` file may be necessary; contributions via pull requests are welcome.

## Licenses

This project is licensed under the GNU General Public License v3.0. Additional components, such as the WPF UI library, are licensed under the MIT License. For detailed licensing information, refer to the LICENSE and LICENSE-MIT files.
