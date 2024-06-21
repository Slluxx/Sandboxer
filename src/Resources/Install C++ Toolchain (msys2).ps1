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

$sourceUrl = "https://repo.msys2.org/distrib/msys2-x86_64-latest.exe"
$destinationPath = "C:\Users\WDAGUtilityAccount\Desktop\msys2-x86_64-latest.exe"
$webClient = New-Object System.Net.WebClient
$webClient.DownloadFile($sourceUrl, $destinationPath)
$webClient.Dispose()

C:\Users\WDAGUtilityAccount\Desktop\msys2-x86_64-latest.exe in --confirm-command --accept-messages --root C:/msys64


Start-Process -FilePath "C:\msys64\msys2_shell.cmd" -ArgumentList "-defterm -no-start -c `"pacman -S --needed base-devel mingw-w64-ucrt-x86_64-toolchain --noconfirm`"" -Wait


$currentPath = [System.Environment]::GetEnvironmentVariable("Path", [System.EnvironmentVariableTarget]::Machine)
$newPath = "C:/msys64/ucrt64/bin"
$updatedPath = "$currentPath;$newPath"
[System.Environment]::SetEnvironmentVariable("Path", $updatedPath, [System.EnvironmentVariableTarget]::Machine)
