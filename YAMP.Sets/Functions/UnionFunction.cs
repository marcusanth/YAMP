using System;
using YAMP;
using System.Collections.Generic;
using YAMP.Exceptions;

namespace YAMP.Sets
{
    [Description("The Union function.")]
    [Kind(PopularKinds.Function)]
    sealed class UnionFunction : ArgumentFunction
	{
        [Description("Create a new Set, with the Union of Sets")]
        [Arguments(1, 1)]
        public SetValue Function(SetValue set1, ArgumentsValue args)
        {
            string newName = string.Format("({0}", set1.Name);

            var newSet = set1.Copy() as SetValue;

            int iArgs = 1; //First is "name"
            foreach(var arg in args)
            {
                iArgs++;
                SetValue set = arg as SetValue;
                newName += string.Format("+{0}", set.Name);
                if (set != null)
                    newSet.Set.UnionWith(set.Set);
                else
                    throw new YAMPArgumentInvalidException("Element is not SetValue", iArgs);
            }
            newName += ")";
            newSet.Name = newName;

            return newSet;
        }

	}
}

