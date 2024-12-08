
namespace Interlace
{
    public partial class TransactionThread
    {
        private class Transaction : ITransaction
        {
            private TransactionHandler handler;

            public Transaction(TransactionHandler handler)
            {
                this.handler = handler;
            }

            public void Invoke(TransactionResult result)
            {
                handler.Invoke(result);
            }
        }

        private class Transaction<T> : ITransaction<T>
        {
            private TransactionHandler<T> handler;

            public Transaction(TransactionHandler<T> handler)
            {
                this.handler = handler;
            }

            public void Invoke(TransactionResult<T> result)
            {
                handler.Invoke(result);
            }
        }
    }
}
