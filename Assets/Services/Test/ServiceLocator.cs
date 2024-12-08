namespace Interlace.Test
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        private static ServiceLocator instance { get => _instance ?? (_instance = new ServiceLocator()); }

        //public delegate void InjectionHandler<T>(T operations);

        //public static InjectionHandler<TestService.ServiceOperations> TestOperations;

        protected internal static TestService.ServiceOperations TestOperations { get; private set; }=new TestService.ServiceOperations();

        private ServiceLocator()
        {
            onServiceLoad();
        }

        protected virtual void onServiceLoad()
        {

        }
    }

    public class TestService
    {
        public delegate void TestCommand();

        //internal static ServiceOperations operations;

        public static void Command()
        {
            ServiceLocator.TestOperations.CommandHandler?.Invoke();
        }

        public class ServiceOperations
        {
            public TestCommand CommandHandler { get; set; }
        }
    }
}
