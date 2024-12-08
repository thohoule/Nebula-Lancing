

namespace Interlace.UmbraTest
{
    public partial class UmbraService
    {
        public static class ClientLocal
        {
            public static void ClientOnlyMethod()
            {
                instance.OnRPCMethod();
            }
        }
    }
}
