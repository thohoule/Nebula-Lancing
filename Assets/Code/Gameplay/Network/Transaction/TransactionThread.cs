using UnityEngine;

namespace Assets.Code.Gameplay.Network
{
    public class TransactionThread
    {
        private ITransaction currentTransaction;

        public bool IsLocked { get; private set; }

        public void Begin(TransactionHandler handler)
        {
            if (handler == null)
            {
                Debug.LogError("TransactionThread: Handler cannot be null.");
                return;
            }

            begin(new Transaction(handler));
        }

        public void Begin<T>(TransactionHandler<T> handler)
        {
            if (handler == null)
            {
                Debug.LogError("TransactionThread: Handler cannot be null.");
                return;
            }

            begin(new Transaction<T>(handler));
        }

        private void begin(ITransaction transaction)
        {
            if (IsLocked)
            {
                Debug.LogError("TransactionThread: Thread is locked, must be released before begining another.");
                return;
            }

            IsLocked = true;
            currentTransaction = transaction;
        }

        public void End(TransactionResult result)
        {
            if (!IsLocked)
            {
                Debug.LogError("TransactionThread: No thread to release.");
                return;
            }

            IsLocked = false;

            if (currentTransaction is Transaction)
                (currentTransaction as Transaction).Invoke(result);
            else
                Debug.LogError("TransactionThread: Handler and expected result mismatch.");
        }

        public void End<T>(TransactionResult<T> result)
        {
            if (!IsLocked)
            {
                Debug.LogError("TransactionThread: No thread to release.");
                return;
            }

            IsLocked = false;

            if (currentTransaction is Transaction<T>)
                (currentTransaction as Transaction<T>).Invoke(result);
            else
                Debug.LogError("TransactionThread: Handler generic type mismatch.");
        }

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

        private interface ITransaction
        {

        }

        private interface ITransaction<T> : ITransaction
        {

        }
    }
}
