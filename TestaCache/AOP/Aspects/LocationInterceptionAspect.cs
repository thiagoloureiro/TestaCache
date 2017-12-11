using System;

namespace TestaCache.AOP.Aspects
{
    /// <summary>
    /// Aspect that, when applied on a property, intercepts invocations of Get and Set semantics.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class LocationInterceptionAspect : Aspect, ILocationInterceptionAspect
    {
        /// <summary>
        /// Method invoked instead of the Get semantic of the property to which the current aspect is applied.
        /// </summary>
        /// <param name="args">Property arguments.</param>
        public virtual void OnGetValue(LocationInterceptionArgs args)
        {
            args.ProceedGetValue();
        }

        /// <summary>
        /// Method invoked instead of the Set semantic of the property to which the current aspect is applied.
        /// </summary>
        /// <param name="args">Property arguments.</param>
        public virtual void OnSetValue(LocationInterceptionArgs args)
        {
            args.ProceedSetValue();
        }
    }
}