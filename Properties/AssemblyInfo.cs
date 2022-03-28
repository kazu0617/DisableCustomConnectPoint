using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: ComVisible(false)]
[assembly: AssemblyTitle(DisableCustomConnectPoint.BuildInfo.Name)]
[assembly: AssemblyDescription(DisableCustomConnectPoint.BuildInfo.Description)]
[assembly: AssemblyCompany("net.kazu0617")]
[assembly: AssemblyProduct(DisableCustomConnectPoint.BuildInfo.GUID)]
[assembly: AssemblyVersion(DisableCustomConnectPoint.BuildInfo.Version)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
namespace DisableCustomConnectPoint
{
	public static class BuildInfo
	{
		public const string Version = "1.1.0";

		public const string Name = "Disable CustomConnector for LogiX";
		public const string Description = "disables custom input/output attributes for LogiX nodes";

		public const string Author = "kazu0617";

		public const string Link = "https://github.com/kazu0617/DisableCustomConnectPoint";

		public const string GUID = "net.kazu0617.DisableCustomConnectPoint";
	}
}
