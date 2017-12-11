using System;

namespace TestaCache.AOP.Aspects
{
    /// <summary>
    /// Base class for all aspects that are declared
    /// </summary>
    public abstract class Aspect : Attribute, IAspect
    {
        /// <summary>
        /// Gets or sets the weaving priority of the aspect.
        /// </summary>
        public int AspectPriority { get; set; }
    }
}