﻿using System;

namespace YAMP
{
    [Description("Deletes variables from memory.")]
    class ClearFunction : ArgumentFunction
    {
        public ClearFunction() : base(1)
        {
        }

        [Description("Clears all variables.")]
        [ExampleAttribute("clear()")]
        public StringValue Function()
        {
            var count = Tokens.Instance.Variables.Count;
            Tokens.Instance.Variables.Clear();
            return new StringValue(count + " objects cleared.");
        }

        [Description("Clears the specified variables given with their names as strings.")]
        [ExampleAttribute("clear(\"x\")", "Deletes the variable x.")]
        [ExampleAttribute("clear(\"x\", \"y\", \"z\")", "Deletes the variables x, y and z.")]
        public StringValue Function(ArgumentsValue args)
        {
            var count = 0;

            foreach (var arg in args.Values)
            {
                if (arg is StringValue)
                {
                    var name = (arg as StringValue).Value;

                    if (Tokens.Instance.Variables.ContainsKey(name))
                    {
                        Tokens.Instance.Variables.Remove(name);
                        count++;
                    }
                }
            }

            return new StringValue(count + " objects cleared.");
        }
    }
}
