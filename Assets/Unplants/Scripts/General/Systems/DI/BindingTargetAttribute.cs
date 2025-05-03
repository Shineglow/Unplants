using System;
using UnityEngine.Scripting;

namespace Unplants.Scripts.General.Systems.DI
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public class BindingTargetAttribute : PreserveAttribute { }
}
