namespace Unplants.General.Systems.DragDropSystem
{
    public interface IDragDropSystem<T1> where T1 : IDragDropListener
    {
        void Add(T1 item);
        void Remove(T1 item);
    }
}
