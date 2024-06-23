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

#! SANDBOXER: <attributes>
#! SANDBOXER:   <checkbox checked="True" uniquePSArgName="chromeInstall">Chrome</checkbox>
#! SANDBOXER:   <checkbox checked="False" uniquePSArgName="firefoxInstall">Firefox</checkbox>
#! SANDBOXER:   <checkbox checked="False" uniquePSArgName="edgeInstall">Edge</checkbox>
#! SANDBOXER: </attributes>

param (
    [Parameter(Mandatory=$true)]
    [string]$chromeInstall,

    [Parameter(Mandatory=$true)]
    [string]$firefoxInstall,

    [Parameter(Mandatory=$true)]
    [string]$edgeInstall
)

$progressPreference = 'silentlyContinue'

if ($chromeInstall -eq "True") {
    $binary = [Environment]::GetFolderPath("Desktop")+"\chrome.exe"
    Write-Host "Downloading Chrome..."
    Invoke-WebRequest -Uri "https://dl.google.com/tag/s/appguid%3D%7B8A69D345-D564-463C-AFF1-A69D9E530F96%7D%26iid%3D%7BFE520AD3-34E0-0C13-62A4-B712B7B4D913%7D%26lang%3Dde%26browser%3D3%26usagestats%3D0%26appname%3DGoogle%2520Chrome%26needsadmin%3Dprefers%26ap%3Dx64-statsdef_1%26installdataindex%3Dempty/update2/installers/ChromeSetup.exe" -OutFile $binary
    Write-Host "Installing Chrome..."
    Start-Process -FilePath $binary -ArgumentList '/silent /install' -Wait
    Remove-Item -Path $binary
}

if ($firefoxInstall -eq "True") {
    # Dont know why but FF doesnt want to be downloaded into .\
    $binary = [Environment]::GetFolderPath("Desktop")+"\ff.exe"
    Write-Host "Downloading Firefox..."
    Invoke-WebRequest -Uri "https://download.mozilla.org/?product=firefox-latest&os=win64&lang=en-US" -OutFile $binary
    Write-Host "Installing Firefox..."
    Start-Process -FilePath $binary -ArgumentList "/S" -Wait
    Remove-Item $binary
}

if ($edgeInstall -eq "True") {
    $binary = [Environment]::GetFolderPath("Desktop")+"\edge.msi"
    Write-Host "Downloading Edge..."
    Invoke-WebRequest -Uri "https://go.microsoft.com/fwlink/?LinkID=2093437" -OutFile $binary
    Write-Host "Installing Edge..."
    Start-Process -FilePath $binary -ArgumentList "/quiet" -Wait
    Remove-Item $binary
}

