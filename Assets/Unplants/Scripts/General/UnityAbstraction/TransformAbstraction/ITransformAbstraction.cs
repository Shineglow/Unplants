using UnityEngine;

namespace Unplants.General.UnityAbstraction.TransformAbstraction
{
    public interface ITransformAbstraction
    {
        Vector3 Position { get; }
        Quaternion Rotation { get; }
        Vector3 Scale { get; }
    }
}
