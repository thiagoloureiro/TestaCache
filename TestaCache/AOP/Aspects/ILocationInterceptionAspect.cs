namespace TestaCache.AOP.Aspects
{
    /// <summary>
    /// Base interface for all aspects which should applied to properties.
    /// </summary>
    internal interface ILocationInterceptionAspect : IAspect
    {
        /// <summary>
        /// Method invoked instead of the Get semantic of the property to which the current aspect is applied.
        /// </summary>
        /// <param name="args">Property arguments.</param>
        void OnGetValue(LocationInterceptionArgs args);

        /// <summary>
        /// Method invoked instead of the Set semantic of the property to which the current aspect is applied.
        /// </summary>
        /// <param name="args">Property arguments.</param>
        void OnSetValue(LocationInterceptionArgs args);
    }
}