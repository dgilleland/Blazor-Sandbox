namespace WebApp;

using System.Collections.Concurrent;
using static System.IO.Directory;
using static System.IO.File;


/// <summary>
/// From SO:
/// https://stackoverflow.com/a/34580159
/// </summary>
public class FileSystemCrawlerSO
{
    public int NumFolders { get; set; }
    private readonly ConcurrentQueue<DirectoryInfo> folderQueue = new ConcurrentQueue<DirectoryInfo>();
    private readonly ConcurrentBag<Task> tasks = new ConcurrentBag<Task>();

    public void CollectFolders(string path)
    {

        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        tasks.Add(Task.Run(() => CrawlFolder(directoryInfo)));

        Task taskToWaitFor;
        while (tasks.TryTake(out taskToWaitFor))
            taskToWaitFor.Wait();
    }


    private void CrawlFolder(DirectoryInfo dir)
    {
        try
        {
            DirectoryInfo[] directoryInfos = dir.GetDirectories();
            foreach (DirectoryInfo childInfo in directoryInfos)
            {
                // here may be dragons using enumeration variable as closure!!
                DirectoryInfo di = childInfo;
                tasks.Add(Task.Run(() => CrawlFolder(di)));
            }
            // Do something with the current folder
            // e.g. Console.WriteLine($"{dir.FullName}");
            NumFolders++;
        }
        catch (Exception ex)
        {
            while (ex != null)
            {
                Console.WriteLine($"{ex.GetType()} {ex.Message}\n{ex.StackTrace}");
                ex = ex.InnerException;
            }
        }
    }
}

/*====================================================*/
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
