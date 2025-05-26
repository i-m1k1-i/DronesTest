namespace DronesTest
{
    public interface IPoolReleaser<T>
    {
        void Release(T item);
    }
}
