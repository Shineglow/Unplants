namespace Unplants.General.Systems.DragDropSystem
{
    public interface IDropListener<T1> where T1 : IDragDropItem
    {
        bool Drop(T1 item);
    }
}
