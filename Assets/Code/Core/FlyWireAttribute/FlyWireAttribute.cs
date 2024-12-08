using System;
using System.Reflection;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;

namespace Assets.Code
{
    [AttributeUsage(AttributeTargets.Field)]
    public class FlyWireAttribute : Attribute
    {
        public FlyWireAttribute(string eventName)
        {
        }

        public FlyWireAttribute(EventInfo eventInfo)
        {

        }
    }
}
