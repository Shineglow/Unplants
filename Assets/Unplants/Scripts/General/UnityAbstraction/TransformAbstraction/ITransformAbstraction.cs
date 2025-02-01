﻿using UnityEngine;

namespace Unplants.General.UnityAbstraction.TransformAbstraction
{
    public interface ITransformAbstraction : ITransformAbstractionGet
    {
        new Vector3 Position { get; set; }
        new Quaternion Rotation { get; set; }
        new Vector3 Scale { get; set; }
    }
}
