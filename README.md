# A proxy class for accessing ZenWorks SOAP services from Powershell

I've run into issues accessing the ZenWorks semi-documented SOAP interface<sup>[1](#footnote1)</sup> using Powershell's
[`New-WebserviceProxy`](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.management/new-webserviceproxy?view=powershell-5.1) 
dynamic class builder Cmdlet. 

While simpler use cases like calling a method without 
parameters could be handled very well, more complex calls involving parameters using Zenworks-SOAP-specific
classes inexorably failed as the classes dynamically created by `New-WebServiceProxy` seemed to be missing
important properties.

So I've decided to try Visual Studio's Service References instead. And indeed, these worked much better. 

I've set up this DLL project, mostly with service reference definitions for selected Zenworks SOAP interfaces 
I needed most often in my integration projects and added some glue code to facilitate the code's use in Powershell.

# Installation
Just grab the Zenworks.DLL from the _Releases_ section 

# Example
    # Load the DLL into your Powershell session
    Add-Type -Path "$PSScriptRoot\bin\Zenworks.dll"
         
    # Create a [Zenworks.Connection] instance using valid Zenworks credentials assuming
    # the Zenworks service is running on "localhost" (i.e. code is running on a Zenworks primary)
    $ZCMServer = "localhost"
    $UserName = "SOAPServiceUser"
    $Password = "Use a secure password to connect to your Zenworks service!"
    $ZenworksSOAPConnection = [Zenworks.Connection]::new($ZCMServer, $UserName, $Password)
         
    # Get the properties of the "/Bundles" folder
    $ZenworksSOAPConnection.BundleAdmin.GetByUID("/Bundles")

# Exposed interfaces

| Property | Zenworks SOAP service WSDL |
|---------|----------------------------|
| Zenworks.BundleAdmin | /zenworks-bundleadmin?wsdl |
| Zenworks.SettingsAdmin | /zenworks-settingsadmin?wsdl |
| Zenworks.AssignmentAdmin | /zenworks-assignmentadmin?wsdl |
| Zenworks.DeviceAdmin | /zenworks-deviceadmin?wsdl |
| Zenworks.UserAdmin | /zenworks-useradmin?wsdl |
| Zenworks.AdministratorAdmin  | /zenworks-administratoradmin?wsdl |

-----------

<a name="footnote1">[1]:</a> You can access/download the WSDLs by browsing _https://<your_ZCM_primary_here>/zenworks-wsdls_
and / or browse the `C:\Program Files (x86)\Novell\ZENworks\share\tomcat\webapps` directory on a Windows 
installor the `/opt/novell/zenworks/share/tomcat/webapps` directory on a Linux install of your Zenworks 
primary to check on all the SOAP services available.
