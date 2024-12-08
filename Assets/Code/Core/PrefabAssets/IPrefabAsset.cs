
namespace Assets.Code
{
    public interface IPrefabAsset
    {
        string PrefabName { get; }
        bool NameSearchEnabled { get; }
        bool TypeSearchEnabled { get; }
    }
}
