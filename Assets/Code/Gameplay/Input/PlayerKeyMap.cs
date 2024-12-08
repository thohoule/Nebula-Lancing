using System;
using System.Collections.Generic;
using UnityEngine;
using TeaSteep;

namespace Assets.Code
{
    public class PlayerKeyMap : KeyMap
    {
        private const string config_key = "KeyMap";

        public const string Forward_Command = "PlayerForward";
        public const string Backward_Command = "PlayerBackward";
        public const string Left_Command = "PlayerLeft";
        public const string Right_Command = "PlayerRight";

        public PlayerKeyMap()
        {
            SetDefaults();
        }

        public void LoadMap()
        {
            var configString = PlayerPrefs.GetString(config_key);

            if (configString != null && configString.Length != 0)
            {
                foreach (var pair in JsonUtility.FromJson<Dictionary<string, Keys>>(configString))
                {
                    SetKeyCode(pair.Key, pair.Value);
                }
            }
        }

        public virtual void SetDefaults()
        {
            SetKeyCode(Forward_Command, Keys.Pressed(KeyCode.W, KeyCode.UpArrow));
            SetKeyCode(Backward_Command, Keys.Pressed(KeyCode.S, KeyCode.DownArrow));
            SetKeyCode(Left_Command, Keys.Pressed(KeyCode.A, KeyCode.LeftArrow));
            SetKeyCode(Right_Command, Keys.Pressed(KeyCode.D, KeyCode.RightArrow));
        }
    }
}
