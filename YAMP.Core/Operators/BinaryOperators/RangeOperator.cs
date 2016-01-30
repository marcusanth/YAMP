using System;
using System.Collections;
using System.Collections.Generic;

namespace YAMP
{
    /// <summary>
    /// This is the class for the range operator : - this one is also
    /// available as a stand-alone expression.
    /// </summary>
	class RangeOperator : BinaryOperator
	{
		const string END = "end";

		public RangeOperator () : base(":", 3)
		{
			IsRightToLeft = true;
		}
		
		public override Value Perform (Value left, Value right)
		{
			var step = 1.0;
			var explicitStep = false;
			var start = 0.0;
			var end = 0.0;
			var all = false;
			
			if(left is ScalarValue)
				start = (left as ScalarValue).Re;
			else if (left is RangeValue)
			{
				var m = left as RangeValue;
				start = m.Start;
				step = m.End;
				explicitStep = true;
			}
			else
				throw new YAMPOperationInvalidException(":", left);

			if(right is ScalarValue)
				end = (right as ScalarValue).Re;
			else if (right is RangeValue)
			{
				var m = right as RangeValue;
				step = m.Start;
				end = m.End;
				all = m.All;
				explicitStep = true;
			}
			else if(right is StringValue)
				all = (right as StringValue).Value.Equals(END);
			else
				throw new YAMPOperationInvalidException(":", left);

			if(!all && !explicitStep && end < start)
				step = -1.0;

			if(all)
				return new RangeValue(start, step);

			return new RangeValue(start, end, step);
		}

		public override Value Handle(Expression left, Expression right, Dictionary<string, Value> symbols)
		{
			var l = left.Interpret(symbols);
			var r = new StringValue(END) as Value;

			if(!(right is SymbolExpression) || !((right as SymbolExpression).SymbolName.Equals(END)))
				r = right.Interpret(symbols);

			return Perform(l, r);
		}

        public override Operator Create()
        {
            return new RangeOperator();
        }
	}
}
