using System.Reflection;
using TestaCache.AOP.Aspects;

namespace TestaCache.AOP.Core.Methods
{
    /// <summary>
    ///  Arguments of aspect which intercept a method without return value.
    /// </summary>
    internal class ActionInterceptionArgs : MethodInterceptionArgs
    {
        private readonly LateBoundAction _action;
        private readonly object[] _argsValues;

        public ActionInterceptionArgs(object instance, MethodInfo method, object[] argsValues, LateBoundAction action)
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
            _action.Invoke(_argsValues);
        }
    }
}