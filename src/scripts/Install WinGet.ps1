# Copyright (c) Slluxx
#
# This program is free software; you can redistribute it and/or modify it
# under the terms and conditions of the GNU General Public License,
# version 3, as published by the Free Software Foundation.
# 
# This program is distributed in the hope it will be useful, but WITHOUT
# ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
# FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for
# more details.
# 
# You should have received a copy of the GNU General Public License
# along with this program.  If not, see <http://www.gnu.org/licenses/>.

$progressPreference = 'silentlyContinue' # way faster download speeds https://github.com/PowerShell/PowerShell/issues/2138
Write-Host "Downloading WinGet and its dependencies... (1/3)"
Invoke-WebRequest -Uri https://aka.ms/getwinget -OutFile Microsoft.DesktopAppInstaller_8wekyb3d8bbwe.msixbundle
Write-Host "Downloading WinGet and its dependencies... (2/3)"
Invoke-WebRequest -Uri https://aka.ms/Microsoft.VCLibs.x64.14.00.Desktop.appx -OutFile Microsoft.VCLibs.x64.14.00.Desktop.appx
Write-Host "Downloading WinGet and its dependencies... (3/3)"
Invoke-WebRequest -Uri https://github.com/microsoft/microsoft-ui-xaml/releases/download/v2.8.6/Microsoft.UI.Xaml.2.8.x64.appx -OutFile Microsoft.UI.Xaml.2.8.x64.appx
Write-Host "Installing WinGet and its dependencies... (1/3)"
Add-AppxPackage Microsoft.VCLibs.x64.14.00.Desktop.appx
Write-Host "Installing WinGet and its dependencies... (2/3)"
Add-AppxPackage Microsoft.UI.Xaml.2.8.x64.appx
Write-Host "Installing WinGet and its dependencies... (3/3)"
Add-AppxPackage Microsoft.DesktopAppInstaller_8wekyb3d8bbwe.msixbundle

Remove-Item Microsoft.DesktopAppInstaller_8wekyb3d8bbwe.msixbundle
Remove-Item Microsoft.VCLibs.x64.14.00.Desktop.appx
Remove-Item Microsoft.UI.Xaml.2.8.x64.appx