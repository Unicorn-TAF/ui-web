﻿namespace Unicorn.Core.Testing.Assertions
{
    public abstract class TypeSafeMatcher<T> : Matcher
    {
        public override bool Matches(object obj)
        {
            return this.IsNotNull(obj) && this.CouldBeCasted(obj) && this.Assertion(obj);
        }

        protected bool CouldBeCasted(object obj)
        {
            bool couldBeCasted = obj is T;

            if (!couldBeCasted)
            {
                MatcherOutput.Append($"was not of type {typeof(T)}");
            }

            return couldBeCasted;
        }

        protected abstract bool Assertion(object obj);
    }
}
