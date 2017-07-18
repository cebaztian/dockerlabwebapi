FROM microsoft/iis

# Adding a user so i can connect trough IIS Manager
# RUN	NET LOCALGROUP "Administrators" "sebas" /add

# Metadata indicating an image maintainer.
MAINTAINER Ana Franco sebastian.burgos@ceiba.com.co

# Install tools and features
RUN powershell Install-WindowsFeature Web-Mgmt-Service
RUN powershell Install-WindowsFeature Web-Windows-Auth
RUN powershell Install-WindowsFeature NET-Framework-45-ASPNET
RUN powershell Install-WindowsFeature Web-Asp-Net45
RUN powershell Install-WindowsFeature NET-WCF-HTTP-Activation45

RUN powershell -NoProfile -Command \
Set-ItemProperty -Path HKLM:\SOFTWARE\Microsoft\WebManagement\Server -Name EnableRemoteManagement -Value 1

# Folder to copy site files
RUN mkdir C:\site

# Grant Permissions
RUN icacls "C:\site\*" /grant everyone:(OI)(CI)F /T

# Creating site on IIS
RUN powershell -NoProfile -Command \
    Import-module IISAdministration; \
    New-IISSite -Name "Site" -PhysicalPath C:\site -BindingInformation "*:8000:"

# define internal port
EXPOSE 8000

# copy local files to container folder
ADD . C:\site