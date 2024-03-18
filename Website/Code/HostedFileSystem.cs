namespace WebApp;
using static System.IO.Directory;
using static System.IO.File;

public class HostedFileSystem
{
    public enum OSPath { WindowsCompatible, LinuxCompatible }
    public enum Recurse { None, Always }

    #region Static fields
    private static string _CurrentDirectory = GetCurrentDirectory();
    private static string _PathRoot = GetDirectoryRoot(_CurrentDirectory);
    private static OSPath _HostFileSystem = _PathRoot.Length == 1 ? OSPath.LinuxCompatible : OSPath.WindowsCompatible;

    private static IEnumerable<string> _ExcludedWinDirectories = new string[] { @"C:\Windows", @"C:\Program Files", @"C:\Program Files (x86)", @"C:\ProgramData", @"C:\Users", @"C:\$Recycle.Bin", @"C:\System Volume Information", @"C:\pagefile.sys", @"C:\hiberfil.sys", @"C:\Config.Msi" };
    private static IEnumerable<string> _ExcludedNixDirectories = new string[] { @"/proc", @"/sys", @"/dev", @"/boot", @"/tmp", @"/run", @"/home", @"/root", @"/etc", @"/var", @"/opt", @"/srv", @"/bin", @"/sbin", @"/lib", @"/lib64", @"/usr", @"/var" };

    private static EnumerationOptions _Recursive = new EnumerationOptions
    {
        IgnoreInaccessible = true,
        RecurseSubdirectories = true
    };
    private static EnumerationOptions _NonRecursive = new EnumerationOptions
    {
        IgnoreInaccessible = true,
        RecurseSubdirectories = false
    };
    #endregion

    #region Static Properties
    public static string CurrentDirectory => _CurrentDirectory;
    public static string AppRoot => _PathRoot;
    public static OSPath HostFileSystem => _HostFileSystem;
    public static IEnumerable<string> ExcludedFolders => HostFileSystem == OSPath.WindowsCompatible ? _ExcludedWinDirectories : _ExcludedNixDirectories;
    #endregion

    #region Static Methods
    public static IEnumerable<string> EnumerateDirectories(string searchPattern = "*", Recurse recursionOption = Recurse.None)
    {
        var enumOption = recursionOption == Recurse.None ? _Recursive : _NonRecursive;
        return Directory.EnumerateDirectories(AppRoot, searchPattern, enumOption).Where(dir => !ExcludedFolders.Any(dir.Contains));
    }
    #endregion
}
