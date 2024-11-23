
namespace DemoProject.EventSystem
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T item);
    }
}
