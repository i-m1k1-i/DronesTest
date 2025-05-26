namespace DronesTest
{
    public interface IPoolHandler<T> : IPoolReleaser<T>, IPoolGetter<T>
    {
    }
}
