using System.Collections.Generic;
using UnityEngine;
using TeaSteep.Character;

namespace Interlace
{
    public class AgentControl : MonoBehaviour
    {
        internal Dictionary<int, IActor> Agents;

        private void Awake()
        {
            Agents = new Dictionary<int, IActor>();
        }
    }
}
