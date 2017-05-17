namespace MathematicLib
{
    public interface IMathResult<out T>
    {
        T GetResult();
    }
}
