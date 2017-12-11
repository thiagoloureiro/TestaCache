using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TestaCache.AOP.Aspects;

namespace TestaCache.AOP.Core.Methods
{
    internal class InterceptionAspectGenerator
    {
        private readonly BindingRestrictions _rule;
        private readonly IEnumerable<MethodInterceptionAspect> _aspects;
        private readonly MethodInterceptionArgs _aspectArgs;
        private readonly bool _isRetValue;
        private readonly bool _isByRefArgs;
        private readonly IEnumerable<DynamicMetaObject> _originalArgs;

        public InterceptionAspectGenerator(object instance, BindingRestrictions rule,
            IEnumerable<MethodInterceptionAspect> aspects, MethodInfo method,
            IEnumerable<DynamicMetaObject> args, IEnumerable<Type> argsTypes)
        {
            _rule = rule;
            _aspects = aspects;
            _originalArgs = args;
            var argsValues = args.Select(x => x.Value).ToArray();
            _isRetValue = method.ReturnType != typeof(void);
            _isByRefArgs = argsTypes.Any(t => t.IsByRef);

            if (_isByRefArgs)
            {
                if (_isRetValue) _aspectArgs = new FuncInterceptionRefArgs(instance, method, argsValues, DelegateFactory.CreateDelegate(instance, method));
                else _aspectArgs = new ActionInterceptionRefArgs(instance, method, argsValues, DelegateFactory.CreateDelegate(instance, method));
            }
            else
            {
                if (_isRetValue) _aspectArgs = new FuncInterceptionArgs(instance, method, argsValues, DelegateFactory.CreateFunction(instance, method));
                else _aspectArgs = new ActionInterceptionArgs(instance, method, argsValues, DelegateFactory.CreateMethodCall(instance, method));
            }
        }

        public DynamicMetaObject Generate()
        {
            var retType = _isRetValue ? _aspectArgs.Method.ReturnType : typeof(object);

            ParameterExpression retValue = Expression.Parameter(retType);
            Expression aspectArgsEx = Expression.Constant(_aspectArgs);
            var aspectCalls = GenerateAspectCalls(_aspects, aspectArgsEx, retValue);

            Expression method = aspectCalls.First();

            for (int i = 1; i < aspectCalls.Count; i++)
            {
                method = Expression.Block(aspectCalls[i], method);
            }

            if (_isByRefArgs) method = method.UpdateRefParamsByArguments(_originalArgs, aspectArgsEx);

            return new DynamicMetaObject(Expression.Block(new[] { retValue }, method, Expression.Convert(retValue, typeof(object))), _rule);
        }

        private List<Expression> GenerateAspectCalls(IEnumerable<MethodInterceptionAspect> aspects, Expression aspectArgsEx, ParameterExpression retValue)
        {
            var calls = new List<Expression>();
            foreach (var aspect in aspects)
            {
                MethodCallExpression aspectExpr = Expression.Call(Expression.Constant(aspect),
                    typeof(MethodInterceptionAspect).GetMethod("OnInvoke"), aspectArgsEx);

                if (_isRetValue)
                {
                    calls.Add(
                        Expression.Block(aspectExpr,
                        Expression.Assign(retValue,
                        Expression.Convert(
                        Expression.Call(aspectArgsEx, typeof(MethodInterceptionArgs).GetProperty("ReturnValue").GetGetMethod()), retValue.Type))));
                }
                else
                {
                    calls.Add(aspectExpr);
                }
            }
            return calls;
        }
    }
}