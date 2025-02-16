﻿using UnityEngine;

namespace Unplants.General.Systems.DragDropSystem
{
    public struct PointerData : IPointerData
    {
        public Vector2 PointerScreenPos { get; set; }
        public int DisplayIndex { get; set; }
        public Vector2 PointerDelta { get; set; }
    }
}
