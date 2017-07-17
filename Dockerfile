FROM microsoft/iis

# Metadata indicating an image maintainer.
MAINTAINER Sebastian Burgos sebastian.burgos@live.com

# Install Chocolatey (tools to automate commandline compiling)
 ENV chocolateyUseWindowsCompression false
 RUN @powershell -NoProfile -ExecutionPolicy unrestricted -Command "(iex ((new-object net.webclient).DownloadString('https://chocolatey.org/install.ps1'))) >$null 2>&1" && SET PATH=%PATH%;%ALLUSERSPROFILE%\chocolatey\bin

# Install build tools
 RUN powershell add-windowsfeature web-asp-net45 \
 && choco install microsoft-build-tools -y --allow-empty-checksums -version 14.0.23107.10 \
 && choco install dotnet4.6-targetpack --allow-empty-checksums -y \
 && choco install nuget.commandline --allow-empty-checksums -y \
 && nuget install MSBuild.Microsoft.VisualStudio.Web.targets -Version 14.0.0.3 \
 && nuget install WebConfigTransformRunner -Version 1.0.0.1

 RUN powershell remove-item C:\inetpub\wwwroot\iisstart.*

# Copy files (temporary work folder)
 RUN md c:\build
 WORKDIR c:/build
 COPY . c:/build
 RUN xcopy c:\build c:\inetpub\wwwroot /s