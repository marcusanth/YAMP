﻿namespace YAMP.Physics
{
    using System;
    using YAMP.Exceptions;

    [Description("In mathematics, Gegenbauer polynomials or ultraspherical polynomials C(x) are orthogonal polynomials on the interval [−1,1] with respect to the weight function (1 − x^2)^(α–1/2). They generalize Legendre polynomials and Chebyshev polynomials, and are special cases of Jacobi polynomials. They are named after Leopold Gegenbauer.")]
    [Kind(PopularKinds.Function)]
    class GegenbauerFunction : ArgumentFunction
    {
        [Description("Computes the Gegenbauer polynomial with order n, parameter alpha at the point z.")]
        [Example("gegenbauer(0, 1, 0.5)", "The 0th order polynomial is always 1.0.")]
        [Example("gegenbauer(1, 0.5, 0.25)", "Computes the 1st order polynomial with parameter alpha = 0.5 at the point 0.25, which gives us -0.25.")]
        public ScalarValue Function(ScalarValue n, ScalarValue alpha, ScalarValue z)
        {
            var nn = n.GetIntegerOrThrowException("n", Name);
            return Gegenbauer(nn, alpha.Re, z);
        }

        [Description("Computes the Gegenbauer polynomial with order n, parameter alpha at the points in Z.")]
        [Example("gegenbauer(1, 1, 0:0.1:1)", "The 0th order polynomial is always 1.0.")]
        public MatrixValue Function(ScalarValue n, ScalarValue alpha, MatrixValue Z)
        {
            var nn = n.GetIntegerOrThrowException("n", Name);
            var M = new MatrixValue(Z.DimensionY, Z.DimensionX);

            for (var i = 1; i <= Z.DimensionX; i++)
            {
                for (var j = 1; j <= Z.DimensionY; j++)
                {
                    M[j, i] = Gegenbauer(nn, alpha.Re, Z[j, i]);
                }
            }

            return M;
        }

        #region Algorithm

        public static ScalarValue Gegenbauer(Int32 n, Double alpha, ScalarValue x)
        {
            if (n < 0)
                throw new YAMPArgumentRangeException("n");

            if (alpha <= 0)
                throw new YAMPArgumentRangeException("alpha", 0);

            if (x.Abs() > 1.0)
                throw new YAMPArgumentRangeException("x", -1, 1);

            var C0 = ScalarValue.One;

            if (n == 0)
            {
                return C0;
            }

            var C1 = 2.0 * alpha * x;

            if (n == 1)
            {
                return C1;
            }

            for (var k = 2; k <= n; k++)
            {
                var Ck = (2 * x * (k + alpha - 1) * C1 - (k + 2 * alpha - 2) * C0) / k;
                C0 = C1;
                C1 = Ck;
            }

            return C1;
        }

        #endregion
    }
}
