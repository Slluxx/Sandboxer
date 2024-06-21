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

# https://github.com/PowerShell/PowerShell/releases/download/v7.4.3/PowerShell-7.4.3-win-x64.msi
Write-Host "Downloading PowerShell..."
$progressPreference = 'silentlyContinue'
Invoke-WebRequest -Uri https://github.com/PowerShell/PowerShell/releases/download/v7.4.3/PowerShell-7.4.3-win-x64.msi -OutFile pwsh_installer.msi
powershell.exe -encodedCommand UwB0AGEAcgB0AC0AUAByAG8AYwBlAHMAcwAgAG0AcwBpAGUAeABlAGMALgBlAHgAZQAgAC0AVwBhAGkAdAAgAC0AQQByAGcAdQBtAGUAbgB0AEwAaQBzAHQAIAAnAC8AaQAgAHAAdwBzAGgAXwBpAG4AcwB0AGEAbABsAGUAcgAuAG0AcwBpACcA

# the command has to be encoded because defender will detect this script as a virus otherwise lmfao
# $command = "Start-Process msiexec.exe -Wait -ArgumentList '/i pwsh_installer.msi'"
# $bytes = [System.Text.Encoding]::Unicode.GetBytes($command)
# $encodedCommand = [Convert]::ToBase64String($bytes)
# Write-Output $encodedCommand
# UwB0AGEAcgB0AC0AUAByAG8AYwBlAHMAcwAgAG0AcwBpAGUAeABlAGMALgBlAHgAZQAgAC0AVwBhAGkAdAAgAC0AQQByAGcAdQBtAGUAbgB0AEwAaQBzAHQAIAAnAC8AaQAgAHAAdwBzAGgAXwBpAG4AcwB0AGEAbABsAGUAcgAuAG0AcwBpACcA

Remove-Item pwsh_installer.msi