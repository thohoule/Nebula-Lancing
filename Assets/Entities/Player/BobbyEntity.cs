
namespace Assets.Entities
{
    public class BobbyEntity
    {
        public int Blanch;
        public SteaveEntity Steave;

        public BobbyEntity()
        {
            Steave = new SteaveEntity();
        }
    }

    public class SteaveEntity
    {
        public string Hammer;

        public SteaveEntity()
        {
            Hammer = "Deez";
        }
    }
}
