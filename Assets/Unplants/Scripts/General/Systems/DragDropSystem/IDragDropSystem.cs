namespace Unplants.General.Systems.DragDropSystem
{
    public interface IDragDropSystem<T> where T : IDragDropItem
    {
        void Add<T>(T item);
        void Remove<T>(T item);
    }
}
