using UnityEngine;

namespace Unplants.General.UnityAbstraction.TransformAbstraction
{
    public interface ITransformAbstractionSet
    {
        Vector3 Position { set; }
        Quaternion Rotation { set; }
        Vector3 Scale { set; }
    }
}
