using System;
using System.Reflection;
using TestaCache.AOP.Aspects;

namespace TestaCache.AOP.Core.Methods
{
    /// <summary>
    ///  Arguments of aspect which intercept a method with return value and parameters passed by reference.
    /// </summary>
    internal class FuncInterceptionRefArgs : MethodInterceptionArgs
    {
        private readonly Delegate _func;
        private readonly object[] _argsValues;

        public FuncInterceptionRefArgs(object instance, MethodInfo method, object[] argsValues, Delegate func)
            : base(instance, method, new Arguments(argsValues))
        {
            _func = func;
            _argsValues = argsValues;
        }

        /// <summary>
        ///  Proceeds with invocation of the method that has been intercepted by calling the next node in the chain of invocation,
        /// passing the current <see cref="Arguments"/> to that method
        /// and storing its return value into the property ReturnValue.
        /// </summary>
        public override void Proceed()
        {
            ReturnValue = _func.DynamicInvoke(_argsValues);
        }
    }
}