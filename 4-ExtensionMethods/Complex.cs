using System.Globalization;

namespace ExtensionMethods
{
    using System;

    /// <inheritdoc cref="IComplex"/>
    public class Complex : IComplex
    {
        private readonly double _re;
        private readonly double _im;

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> class.
        /// </summary>
        /// <param name="re">the real part.</param>
        /// <param name="im">the imaginary part.</param>
        public Complex(double re, double im)
        {
            _re = re;
            _im = im;
        }

        /// <inheritdoc cref="IComplex.Real"/>
        public double Real => _re;

        /// <inheritdoc cref="IComplex.Imaginary"/>
        public double Imaginary => _im;

        /// <inheritdoc cref="IComplex.Modulus"/>
        public double Modulus => Math.Sqrt(Math.Pow(Real, 2) + Math.Pow(Imaginary, 2));

        /// <inheritdoc cref="IComplex.Phase"/>
        public double Phase => Math.Atan2(Real, Imaginary);

        /// <inheritdoc cref="IComplex.ToString"/>
        public override string ToString()
        {
            return String.Format("{0}{1}{2}", Real == 0 ? "" : Real.ToString(),
                Imaginary <= 0 ? "" : "+",
                Imaginary == 0 ? "" : (Imaginary.Equals(1) ? "i" :
                    (Imaginary.Equals(-1) ? "-i" : Imaginary.ToString(CultureInfo.CurrentCulture) + "i")));
        }


        public bool Equals(IComplex other)
        {
            return _re.Equals(other.Real) && _im.Equals(other.Imaginary);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Complex) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Real, Imaginary);
        }
    }
}
