using YAMP.Exceptions;

namespace YAMP.Sets
{
    [Description("The NewSet (unordered) function.")]
    [Kind(PopularKinds.Function)]
    sealed class NewSetFunction : ArgumentFunction
	{
        [Description("Creates a new Unordered Set. Arguments may be Strings, Numerics or Matrixes. If Matrix, all its elements will be added")]
        [Arguments(1, 0)]
        public SetValue Function(StringValue name, ArgumentsValue args)
        {
            var set = new SetValue(name.Value, null, false);

            int iArgs = 1; //First is "name"
            foreach (var arg in args)
            {
                iArgs++;
                if (arg is MatrixValue)
                {
                    set.AddElements((arg as MatrixValue).ToArray());
                }
                else if (arg is StringValue)
                {
                    set.Set.Add((arg as StringValue).Value);
                }
                else if (arg is NumericValue)
                {
                    set.Set.Add(arg as NumericValue);
                }
                else
                    throw new YAMPArgumentInvalidException("Element is not ScalarValue, StringValue or MatrixValue", iArgs);
            }
            return set;
        }
	}
}

