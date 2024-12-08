using UnityEngine;
using TeaSteep;

namespace Assets.Code.Gameplay.MainMenuSM
{
    public class MainMenuControl : MonoBehaviour, IBranchContent
    {
        private BranchMachine machine;
        //private StateChangeLock<StateInstance<MainMenuControl>> stateLock;

        //[SerializeField]
        //private TestLobbyState lobbyState;

        #region States

        #region Splash
        public TeaState Splash { get; private set; }
        #endregion

        #region MainMenu
        public TeaState MainMenu { get; private set; }
        #endregion

        #region Options
        public TeaState Options { get; private set; }
        #endregion

        #region Play
        public TeaState Play { get; private set; }
        #endregion

        #region Join
        public TeaState Join { get; private set; }
        #endregion

        #region LobbyTest
        public TeaState LobbyTest { get; private set; }
        #endregion

        #endregion

        //public StateInstance<MainMenuControl> LobbyTest { get; private set; }
        //public StateInstance<MainMenuControl> HostTest { get; private set; }
        //public StateInstance<MainMenuControl> ClientTest { get; private set; }

        private static MainMenuControl instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            machine = BranchMachine.CreateMachine(this);
            //stateLock = new StateChangeLock<StateInstance<MainMenuControl>>();
        }

        private void Start()
        {
            //LobbyTest = new StateInstance<MainMenuControl>(lobbyState, this);

            //stateLock.CurrentState = LobbyTest;
        }
    }
}
