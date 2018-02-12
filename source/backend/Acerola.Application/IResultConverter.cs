namespace Acerola.Application
{
    public interface IResultConverter
    {
        T Map<T>(object source);
    }
}
