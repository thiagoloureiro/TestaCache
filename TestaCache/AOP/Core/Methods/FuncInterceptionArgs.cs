using System.Reflection;
using TestaCache.AOP.Aspects;

namespace TestaCache.AOP.Core.Methods
{
    /// <summary>
    ///  Arguments of aspect which intercept a method with return value.
    /// </summary>
    internal class FuncInterceptionArgs : MethodInterceptionArgs
    {
        private readonly LateBoundFunc _func;
        private readonly object[] _argsValues;

        public FuncInterceptionArgs(object instance, MethodInfo method, object[] argsValues, LateBoundFunc func)
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
            ReturnValue = _func.Invoke(_argsValues);
        }
    }
}