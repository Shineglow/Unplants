using System;
using UnityEngine;

namespace Unplants.General.UnityAbstraction.TransformAbstraction
{
    public sealed class TransformAbstractionBase : ITransformAbstraction
    {
        private Transform _transform;

        public Vector3 Position { get => _transform.position; set => _transform.position = value; }
        public Quaternion Rotation { get => _transform.rotation; set => _transform.rotation = value; }
        public Vector3 Scale { get => _transform.localScale; set => _transform.localScale = value; }

        private TransformAbstractionBase() { }

        public TransformAbstractionBase(Transform transform)
        {
            if (transform == null)
                throw new NullReferenceException($"Trying to initialize {nameof(TransformAbstractionBase)} object with null value of {nameof(transform)} parameter!");

            _transform = transform;
        }
    }
}
