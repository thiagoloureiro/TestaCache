using System;
using System.Reflection;
using TestaCache.AOP.Aspects;

namespace TestaCache.AOP.Core.Methods
{
    /// <summary>
    ///  Arguments of aspect which intercept a method without return value and parameters passed by reference.
    /// </summary>
    internal class ActionInterceptionRefArgs : MethodInterceptionArgs
    {
        private readonly Delegate _action;
        private readonly object[] _argsValues;

        public ActionInterceptionRefArgs(object instance, MethodInfo method, object[] argsValues, Delegate action)
            : base(instance, method, new Arguments(argsValues))
        {
            _action = action;
            _argsValues = argsValues;
        }

        /// <summary>
        /// Proceeds with invocation of the method that has been intercepted by calling the next node in the chain of invocation,
        /// passing the current <see cref="Arguments"/> to that method.
        /// </summary>
        public override void Proceed()
        {
            _action.DynamicInvoke(_argsValues);
        }
    }
}