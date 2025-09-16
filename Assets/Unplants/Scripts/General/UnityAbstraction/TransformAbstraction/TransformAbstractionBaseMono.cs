using UnityEngine;

namespace Unplants.General.UnityAbstraction.TransformAbstraction
{
    public sealed class TransformAbstractionBaseMono : MonoBehaviour, ITransformAbstraction
    {
        public Vector3 Position { get => transform.position; set => transform.position = value; }

        public Quaternion Rotation { get => transform.rotation; set => transform.rotation = value; }

        public Vector3 Scale { get => transform.localScale; set => transform.localScale = value; }
    }
}
