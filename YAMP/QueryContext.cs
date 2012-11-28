/*
	Copyright (c) 2012, Florian Rappl.
	All rights reserved.

	Redistribution and use in source and binary forms, with or without
	modification, are permitted provided that the following conditions are met:
		* Redistributions of source code must retain the above copyright
		  notice, this list of conditions and the following disclaimer.
		* Redistributions in binary form must reproduce the above copyright
		  notice, this list of conditions and the following disclaimer in the
		  documentation and/or other materials provided with the distribution.
		* Neither the name of the YAMP team nor the names of its contributors
		  may be used to endorse or promote products derived from this
		  software without specific prior written permission.

	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
	ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
	WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
	DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
	DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
	(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
	LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
	ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
	(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
	SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Collections.Generic;

namespace YAMP
{
	/// <summary>
	/// Represents the context that is used for the current input query.
	/// </summary>
	public class QueryContext
	{
		#region Members

		string _original;
		string _input;
        ParseTreeCollection statements;

		#endregion

		#region ctor

		/// <summary>
		/// Creates a new query context.
		/// </summary>
		/// <param name="input">The input to parse</param>
		public QueryContext(string input)
		{
			Input = input;
            statements = new ParseTreeCollection(this);
		}

        /// <summary>
        /// Creates a new (underlying) QueryContext
        /// </summary>
        /// <param name="query">The query context to copy</param>
        internal QueryContext(QueryContext query) : this(query.Input)
        {
            Context = new ParseContext(query.Context);
        }

        /// <summary>
        /// Just a stupid dummy!
        /// </summary>
        private QueryContext()
        {
        }

        /// <summary>
        /// Creates a dummy context that just holds the given ParseContext.
        /// </summary>
        /// <param name="context">The ParseContext to contain</param>
        /// <returns>A new (dummy) QueryContext</returns>
        public static QueryContext Dummy(ParseContext context)
        {
            var query = new QueryContext();
            query.Context = context;
            return query;
        }

		#endregion

        #region Properties

        /// <summary>
        /// Gets the result in a string representation.
        /// </summary>
        public string Result
        {
            get
            {
                if (Output == null)
                    return string.Empty;

                return Output.ToString(Context);
            }
        }

		/// <summary>
		/// Gets the input that is being used by the parser.
		/// </summary>
		public string Input
		{
			get { return _input; }
			set
			{
				_original = value;

				if (value == null)
					value = string.Empty;

				_input = value;
			}
		}

		/// <summary>
		/// Gets a boolean indicating whether the result should be printed.
		/// </summary>
        public bool IsMuted
        {
            get { return Output == null; }
        }

		/// <summary>
		/// Gets the original passed input.
		/// </summary>
		public string Original
		{
			get { return _original; }
		}

		/// <summary>
		/// Gets the result of the query.
		/// </summary>
		public Value Output { get; internal set; }

		/// <summary>
		/// Gets the context used for this query.
		/// </summary>
		public ParseContext Context { get; internal set; }

        /// <summary>
        /// Gets the statements generated for this query.
        /// </summary>
        public ParseTreeCollection Statements
        {
            get { return statements; }
        }

		#endregion

		#region Methods

		internal void Interpret(Dictionary<string, Value> values)
		{
            Output = Statements.Run(values);
		}

		public override string ToString()
		{
			return string.Format("{0} ={1}{2}", Input, Environment.NewLine, Statements);
		}

		#endregion
    }
}