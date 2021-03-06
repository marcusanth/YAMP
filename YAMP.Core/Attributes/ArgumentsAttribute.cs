﻿namespace YAMP
{
    using System;

	/// <summary>
	/// The attribute to store information about optional arguments.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class ArgumentsAttribute : Attribute
    {
        #region ctor

        /// <summary>
		/// Creates a new attribute to declare a container for optional arguments.
		/// </summary>
		/// <param name="index">The index that stores optional arguments.</param>
		/// <param name="min">The minimum number of arguments that need to be specified.</param>
		/// <param name="max">The maximum number of arguments that will be delegated to this container.</param>
		/// <param name="delta">The chunks of arguments to include, i.e. 2 is always an even number of arguments.</param>
		public ArgumentsAttribute(int index, int min = 1, int max = int.MaxValue, int delta = 1)
        {
			MinimumArguments = min < 0 ? 0 : min;
			MaximumArguments = max < MinimumArguments ? MinimumArguments : max;
			Index = index;
			StepArguments = delta > 0 ? delta : 1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the minimum number of arguments that need to be provided for the specified parameter.
        /// </summary>
		public Int32 MinimumArguments { get; private set; }

		/// <summary>
		/// Gets the maximum number of arguments that can be provided for the specified parameter.
		/// </summary>
		public Int32 MaximumArguments { get; private set; }

		/// <summary>
		/// Gets the number of arguments that need to be provided starting at MinimumArguments, i.e.
		/// if delta = 2 and min = 0 then either 0, 2, 4, ... arguments can be specified.
		/// </summary>
		public Int32 StepArguments { get; private set; }

		/// <summary>
		/// Gets the index of the parameter that can contain optional arguments.
		/// </summary>
		public Int32 Index { get; private set; }

        #endregion
    }
}
