﻿using System;

namespace YAMP
{
    [Description("Generates a matrix with only ones.")]
    class OnesFunction : ArgumentFunction
    {
        [Description("Generates a 1x1 matrix.")]
        public MatrixValue Function()
        {
            return MatrixValue.Ones(1, 1);
        }

        [Description("Generates an n-dimensional matrix with only ones.")]
        [Example("ones(5)", "Returns a 5x5 matrix with 1 in each cell.")]
        public MatrixValue Function(ScalarValue dim)
        {
            var k = (int)dim.Value;
            return MatrixValue.Ones(k, k);
        }

        [Description("Generates a n-by-m matrix with only ones.")]
        [Example("ones(5,2)", "Returns a 5x2 matrix with 1 in each cell.")]
        public MatrixValue Function(ScalarValue rows, ScalarValue cols)
        {
            var k = (int)rows.Value;
            var l = (int)cols.Value;
            return MatrixValue.Ones(k, l);
        }
    }
}
