
namespace Assets.Code.Gameplay.Network
{
    public class TransactionResult
    {
        public TransactionState State { get; protected set; }
        public string ErrorMessage { get; protected set; }

        public static TransactionResult Successful()
        {
            return new TransactionResult()
            {
                State = TransactionState.Successful
            };
        }

        public static TransactionResult Error(string errorMessage)
        {
            return new TransactionResult()
            {
                State = TransactionState.Error,
                ErrorMessage = errorMessage
            };
        }
    }

    public class TransactionResult<T> : TransactionResult
    {
        public T Context { get; private set; }

        private TransactionResult() { }

        public TransactionResult(T context)
        {
            Context = context;
            State = TransactionState.Successful;
        }

        public new static TransactionResult<T> Error(string errorMessage)
        {
            return new TransactionResult<T>()
            {
                State = TransactionState.Error,
                ErrorMessage = errorMessage
            };
        }
    }

    public enum TransactionState
    {
        Error,
        Canceled,
        Successful
    }
}
