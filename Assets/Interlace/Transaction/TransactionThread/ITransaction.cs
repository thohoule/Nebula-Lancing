
namespace Interlace
{
    public partial class TransactionThread
    {
        private interface ITransaction
        {
        }

        private interface ITransaction<T> : ITransaction
        {

        }
    }
}
