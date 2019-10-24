namespace CAM.Core.SharedKernel
{
    /// <summary>
    /// Base entity class for entities to inherit from.
    /// </summary>
    public abstract class BaseEntity<TId>
    {
        public virtual TId Id { get; set; }
    }
}