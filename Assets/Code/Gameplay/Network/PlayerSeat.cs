using System;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using Assets.Code.Gameplay.GameSM;

namespace Assets.Code.Gameplay.Network
{
    public class PlayerSeat : NetworkBehaviour, ILaneControlHandler
    {
    }

    /// <summary>
    /// Player seat is passed as a handler (client side), this enables that 
    /// players button selections to be passed (it enables UI elements).
    /// </summary>
    public interface ILaneControlHandler
    {

    }
}
