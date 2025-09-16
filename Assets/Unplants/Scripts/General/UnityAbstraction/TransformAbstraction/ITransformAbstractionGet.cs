using UnityEngine;

namespace Unplants.General.UnityAbstraction.TransformAbstraction
{
    public interface ITransformAbstractionGet
    {
        Vector3 Position { get; }
        Quaternion Rotation { get; }
        Vector3 Scale { get; }
    }
}
