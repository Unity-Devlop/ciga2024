using SRDebugger.Services;
using SRDebugger.Services.Implementation;
using SRF.Service;

public static class SRDebug
{
    public const string Version = SRDebugger.VersionInfo.Version;
    private static bool isInit = false;

    public static IDebugService Instance
    {
        get { return SRServiceManager.GetService<IDebugService>(); }
    }

    public static bool IsInit
    {
        get { return isInit; }
    }

    public static void Init()
    {
        isInit = true;

        UnityEngine.Debug.Log("SrDebug Init");

        // Initialize console if it hasn't already initialized.
        SRServiceManager.GetService<IConsoleService>();

        // Load the debug service
        SRServiceManager.GetService<IDebugService>();
    }
}
