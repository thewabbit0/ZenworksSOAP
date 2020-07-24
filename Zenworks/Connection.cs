using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenworks
{
    /**
     *  <summary>
     *  Zenworks <c>Connection</c> class for accessing the Zenworks SOAP interface in Powershell code.
     *  
     *  Exposes selected Service Reference - maintained SOAP classes from the Zenworks SOAP service
     *  as instantiated oroperties of the <c>Connection</c> class.
     *  </summary>
     */
    public class Connection
    {
        /// <summary> <c>BundleAdmin</c> property exposing a pre-connected instance of the <c>BundleAdminServiceClient</c> class</summary>
        public Bundle.BundleAdminServiceClient BundleAdmin;
        /// <summary> <c>SettingsAdmin</c> property exposing a pre-connected instance of the <c>SystemSettingsServiceClient</c> class</summary>
        public SystemSettings.SystemSettingsServiceClient SettingsAdmin;
        /// <summary> <c>AssignmentAdmin</c> property exposing a pre-connected instance of the <c>AssignmentServiceClient</c> class</summary>
        public Assignment.AssignmentServiceClient AssignmentAdmin;
        /// <summary> <c>DeviceAdmin</c> property exposing a pre-connected instance of the <c>DeviceAdminServiceClient</c> class</summary>
        public Device.DeviceAdminServiceClient DeviceAdmin;
        /// <summary> <c>UserAdmin</c> property exposing a pre-connected instance of the <c>UserAdminServiceClient</c> class</summary>
        public User.UserAdminServiceClient UserAdmin;
        /// <summary> <c>AdministratorAdmin</c> property exposing a pre-connected instance of the <c>AdministratorAdminServiceClient</c> class</summary>
        public Administrator.AdministratorAdminServiceClient AdministratorAdmin;
        /// <summary> <c>RegistrationAdmin</c> property exposing a pre-connected instance of the <c>RegistrationAdminServiceClient</c> class</summary>
        public Registration.RegistrationAdminServiceClient RegistrationAdmin;
        /// <summary>The SOAP binding object instance of the Zenworks server connection</summary>
        private readonly System.ServiceModel.BasicHttpBinding ZenworksSOAPBinding;

        /**
         * <summary> Constructor for the <c>Connection</c> class for instantiation from Powershell. 
         * <example> For use in powershell:
         * <code>
         * # Load the DLL into your Powershell session
         * Add-Type -Path "$PSScriptRoot\bin\Zenworks.dll"
         * 
         * # Create a [Zenworks.Connection] instance using valid Zenworks credentials assuming
         * # the Zenworks service is running on "localhost" (i.e. code is running on a Zenworks primary)
         * $ZCMServer = "localhost"
         * $UserName = "SOAPServiceUser"
         * $Password = "Use a secure password to connect to your Zenworks service!"
         * $ZenworksSOAPConnection = [Zenworks.Connection]::new($ZCMServer, $UserName, $Password)
         * 
         * # Get the properties of the "/Bundles" folder
         * $ZenworksSOAPConnection.BundleAdmin.GetByUID("/Bundles")
         * </code>
         * </example>
         * </summary>
         */
        public Connection(string ZenworksServer, string UserName, string Password, string AddressingScheme = "http://")
        {
            // Variable to hold the constructed endpoint address for the creation of a new instance
            // of the respective ServiceClient class. The variable is being re-purposed for each
            // instantiation of 
            System.ServiceModel.EndpointAddress ZenworksSOAPAddress;

            // The HTTP binding needs to be constructed differently depending on whether TLS is in use or not
            if (AddressingScheme == "https://")
            {
                this.ZenworksSOAPBinding = new System.ServiceModel.BasicHttpBinding(System.ServiceModel.BasicHttpSecurityMode.Transport);
            } else {
                this.ZenworksSOAPBinding = new System.ServiceModel.BasicHttpBinding(System.ServiceModel.BasicHttpSecurityMode.TransportCredentialOnly);
            }
            // Credential type is always "Basic" for HTTP basic authentication.
            this.ZenworksSOAPBinding.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.Basic;

            // increase the buffer and message sizes to 1 MB each as the defaults are too small for 
            // the larger response messages returned by the Zenworks web services
            this.ZenworksSOAPBinding.MaxReceivedMessageSize = 1048576;
            this.ZenworksSOAPBinding.MaxBufferSize = 1048576;

            // Initialize the BundleAdmin SOAP object for use with Powershell
            ZenworksSOAPAddress = new System.ServiceModel.EndpointAddress(AddressingScheme + ZenworksServer + "/zenworks-bundleadmin");
            this.BundleAdmin = new Bundle.BundleAdminServiceClient(this.ZenworksSOAPBinding, ZenworksSOAPAddress);
            this.BundleAdmin.ClientCredentials.UserName.UserName = UserName;
            this.BundleAdmin.ClientCredentials.UserName.Password = Password;

            // Initialize the SettingsAdmin SOAP object for use with Powershell
            ZenworksSOAPAddress = new System.ServiceModel.EndpointAddress(AddressingScheme + ZenworksServer + "/zenworks-settingsadmin");
            this.SettingsAdmin = new SystemSettings.SystemSettingsServiceClient(this.ZenworksSOAPBinding, ZenworksSOAPAddress);
            this.SettingsAdmin.ClientCredentials.UserName.UserName = UserName;
            this.SettingsAdmin.ClientCredentials.UserName.Password = Password;

            // Initialize the AssignmentAdmin SOAP object for use with Powershell
            ZenworksSOAPAddress = new System.ServiceModel.EndpointAddress(AddressingScheme + ZenworksServer + "/zenworks-assignmentadmin");
            this.AssignmentAdmin = new Assignment.AssignmentServiceClient(this.ZenworksSOAPBinding, ZenworksSOAPAddress);
            this.AssignmentAdmin.ClientCredentials.UserName.UserName = UserName;
            this.AssignmentAdmin.ClientCredentials.UserName.Password = Password;

            // Initialize the DeviceAdmin SOAP object for use with Powershell
            ZenworksSOAPAddress = new System.ServiceModel.EndpointAddress(AddressingScheme + ZenworksServer + "/zenworks-deviceadmin");
            this.DeviceAdmin = new Device.DeviceAdminServiceClient(this.ZenworksSOAPBinding, ZenworksSOAPAddress);
            this.DeviceAdmin.ClientCredentials.UserName.UserName = UserName;
            this.DeviceAdmin.ClientCredentials.UserName.Password = Password;

            // Initialize the UserAdmin SOAP object for use with Powershell
            ZenworksSOAPAddress = new System.ServiceModel.EndpointAddress(AddressingScheme + ZenworksServer + "/zenworks-useradmin");
            this.UserAdmin = new User.UserAdminServiceClient(this.ZenworksSOAPBinding, ZenworksSOAPAddress);
            this.UserAdmin.ClientCredentials.UserName.UserName = UserName;
            this.UserAdmin.ClientCredentials.UserName.Password = Password;

            // Initialize the AdministratorAdmin SOAP object for use with Powershell
            ZenworksSOAPAddress = new System.ServiceModel.EndpointAddress(AddressingScheme + ZenworksServer + "/zenworks-administratoradmin");
            this.AdministratorAdmin = new Administrator.AdministratorAdminServiceClient(this.ZenworksSOAPBinding, ZenworksSOAPAddress);
            this.AdministratorAdmin.ClientCredentials.UserName.UserName = UserName;
            this.AdministratorAdmin.ClientCredentials.UserName.Password = Password;

            // Initialize the RegistrationAdmin SOAP object for use with Powershell
            ZenworksSOAPAddress = new System.ServiceModel.EndpointAddress(AddressingScheme + ZenworksServer + "/zenworks-registrationadmin");
            this.RegistrationAdmin = new Registration.RegistrationAdminServiceClient(this.ZenworksSOAPBinding, ZenworksSOAPAddress);
            this.RegistrationAdmin.ClientCredentials.UserName.UserName = UserName;
            this.RegistrationAdmin.ClientCredentials.UserName.Password = Password;
        } // Connection() constructor
    } // class Connection 
} // namespace Zenworks
