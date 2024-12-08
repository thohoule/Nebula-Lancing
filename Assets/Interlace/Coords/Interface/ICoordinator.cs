
namespace Interlace
{
    public interface ICoordinator<THandler>
    {
        void Initialize();
        void SetHandler(THandler handler);
    }
}
