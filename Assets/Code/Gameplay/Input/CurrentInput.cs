
namespace Assets.Code.Gameplay
{
    public struct CurrentInput
    {
        public bool ForwardPressed { get; private set; }
        public bool BackwardsPressed { get; private set; }
        public bool LeftPressed { get; private set; }
        public bool RightPressed { get; private set; }

        public static CurrentInput Get()
        {
            return new CurrentInput()
            {
                ForwardPressed = PlayerKeyMap.Get(PlayerKeyMap.Forward_Command),
                BackwardsPressed = PlayerKeyMap.Get(PlayerKeyMap.Backward_Command),
                LeftPressed = PlayerKeyMap.Get(PlayerKeyMap.Left_Command),
                RightPressed = PlayerKeyMap.Get(PlayerKeyMap.Right_Command),
            };
        }
    }
}
