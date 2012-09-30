﻿using System;
using YAMP.Numerics;

namespace YAMP
{
    [Description("Computes the eigenvectors of a given matrix.")]
    class EvFunction : StandardFunction
    {
        [Description("Solves the eigenproblem of a matrix A and return a matrix with all (+degenerate) eigenvectors.")]
        [Example("eig(1,2,3;4,5,6;7,8,9)", "Returns a 3x3 matrix with the three eigenvectors of this 3x3 matrix.")]
        public override Value Perform(Value argument)
        {
            if (argument is MatrixValue)
            {
                var ev = new Eigenvalues(argument as MatrixValue);
                return ev.GetV();
            }

            throw new OperationNotSupportedException("ev", argument);
        }
    }
}
