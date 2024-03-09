namespace WebApp;

public class ClassInfo
{
    private List<string> _ClassNames = new();
    public IEnumerable<string> ClassNames => _ClassNames;
    public ClassInfo(Type classType)
    {
        Type? type = classType;
        while (type is not null)
        {
            _ClassNames.Add(type.Name);
            type = type.BaseType;
        }
    }
}
