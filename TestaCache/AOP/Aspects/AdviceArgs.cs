namespace TestaCache.AOP.Aspects
{
    /// <summary>
    /// Base class for arguments of all advices.
    /// </summary>
    public abstract class AdviceArgs
    {
        protected AdviceArgs(object instance)
        {
            Instance = instance;
        }

        /// <summary>
        /// Gets or sets the object instance on which the method is being executed.
        /// </summary>
        public object Instance { get; private set; }
    }
}