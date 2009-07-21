namespace Moneyero.Conventions
{
    /// <summary>
    /// Represents a convention for an IoC container binding.
    /// </summary>
    public interface IBindConvention
    {
        /// <summary>
        /// Executes the bind convention.
        /// </summary>
        void Execute();
    }
}