namespace Pool
{
    public interface IPoolable
    {
        void OnExitPool();
        void OnEnterPool();
    }
}