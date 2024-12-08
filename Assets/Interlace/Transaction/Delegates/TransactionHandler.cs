
namespace Interlace
{
    public delegate void TransactionHandler(TransactionResult result);
    public delegate void TransactionHandler<T>(TransactionResult<T> result);
}
