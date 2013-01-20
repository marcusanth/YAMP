﻿using System;

namespace YAMP
{
    [Description("Returns a boolean matrix to state if the given numbers are proper numbers.")]
    [Kind(PopularKinds.Logic)]
    class IsNaNFunction : StandardFunction
    {
        protected override ScalarValue GetValue(ScalarValue value)
        {
            return new ScalarValue(double.IsNaN(value.Value) || double.IsNaN(value.ImaginaryValue));
        }
    }
}
