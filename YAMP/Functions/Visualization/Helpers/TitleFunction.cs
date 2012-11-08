﻿using System;

namespace YAMP
{
    class TitleFunction : PropertyFunction<StringValue>
    {
        public TitleFunction() : base("Title")
        {
        }

        protected override object GetValue(StringValue parameter)
        {
            return parameter.Value;
        }

        protected override StringValue GetValue(object original)
        {
            return new StringValue(original.ToString());
        }
    }
}
