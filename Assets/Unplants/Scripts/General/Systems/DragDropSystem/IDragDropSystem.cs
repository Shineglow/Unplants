namespace Unplants.General.Systems.DragDropSystem
{
    public interface IDragDropSystem<T1, T2> where T1 : IDragListener<T2>
    {
        void Add(T1 item);
        void Remove(T1 item);
    }
}
