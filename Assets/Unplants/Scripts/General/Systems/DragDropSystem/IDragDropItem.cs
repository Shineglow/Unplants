using Unplants.General.UnityAbstraction.TransformAbstraction;

namespace Unplants.General.Systems.DragDropSystem
{
    public interface IDragDropItem
    {
        ITransformAbstraction TransformAbstraction { get; }
    }
}
