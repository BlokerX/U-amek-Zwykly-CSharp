using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operators
{
    public class UlamekZwykly
    {
        public UlamekZwykly(int licznik, int mianownik)
        {
            Licznik = licznik;
            Mianownik = mianownik;
        }

        public int Licznik { get; set; }
        public int Mianownik { get; set; }

        public float WartoscCalkowita()
        {
            return Licznik / Mianownik;
        }

        /// <summary>
        /// Skracanie ułamka
        /// </summary>
        /// <param name="ulamekZwykly">Ułamek do skrócenia</param>
        /// <returns></returns>
        public static UlamekZwykly Skroc(UlamekZwykly ulamekZwykly)
        {
            var nwd = NajwiekszyWspolnyDzielnik(ulamekZwykly.Licznik, ulamekZwykly.Mianownik);
            if (nwd != 0)
            {
                return new UlamekZwykly(ulamekZwykly.Licznik / nwd, ulamekZwykly.Mianownik / nwd);
            }
            else
                return ulamekZwykly;
        }

        /// <summary>
        /// Skracanie ułamka
        /// </summary>
        /// <returns></returns>
        public UlamekZwykly Skroc()
        {
            return Skroc(this);
        }

        #region Operatory_i_przeciążenia

        // Operacje na ułamku:

        public static UlamekZwykly operator +(UlamekZwykly a)
            => a;
        public static UlamekZwykly operator -(UlamekZwykly a)
            => new UlamekZwykly(-a.Licznik, a.Mianownik);


        // Działania na ułamkach:

        public static UlamekZwykly operator +(UlamekZwykly a, UlamekZwykly b)
            => Skroc(new UlamekZwykly((a.Licznik * b.Mianownik) + (b.Licznik * a.Mianownik), a.Mianownik * b.Mianownik));

        public static UlamekZwykly operator -(UlamekZwykly a, UlamekZwykly b)
            => Skroc(new UlamekZwykly((a.Licznik * b.Mianownik) - (b.Licznik * a.Mianownik), a.Mianownik * b.Mianownik));

        public static UlamekZwykly operator *(UlamekZwykly a, UlamekZwykly b)
            => Skroc(new UlamekZwykly(a.Licznik * b.Licznik, a.Mianownik * b.Mianownik));

        public static UlamekZwykly operator /(UlamekZwykly a, UlamekZwykly b)
            => Skroc(new UlamekZwykly(a.Licznik * b.Mianownik, a.Mianownik * b.Licznik));


        // Logiczne:

        public static bool operator ==(UlamekZwykly a, UlamekZwykly b)
            => RownaSie(a, b);
        public static bool operator !=(UlamekZwykly a, UlamekZwykly b)
            => !RownaSie(a, b);

        public static bool operator <(UlamekZwykly a, UlamekZwykly b)
            => Mniejszy(a, b);
        public static bool operator >(UlamekZwykly a, UlamekZwykly b)
            => Wiekszy(a, b);

        public static bool operator <=(UlamekZwykly a, UlamekZwykly b)
            => RownaSie(a, b) || Mniejszy(a, b);
        public static bool operator >=(UlamekZwykly a, UlamekZwykly b)
            => RownaSie(a, b) || Wiekszy(a, b);

        private static bool RownaSie(UlamekZwykly u, UlamekZwykly u2)
        {
            var _u = new UlamekZwykly(u.Licznik * u2.Mianownik, u.Mianownik * u2.Mianownik);
            var _u2 = new UlamekZwykly(u2.Licznik * u.Mianownik, u.Mianownik * u2.Mianownik);

            if (_u.Licznik == _u2.Licznik && _u.Mianownik == _u2.Mianownik)
                return true;
            else
                return false;
        }
        private static bool Mniejszy(UlamekZwykly u, UlamekZwykly u2)
        {
            var _u = new UlamekZwykly(u.Licznik * u2.Mianownik, u.Mianownik * u2.Mianownik);
            var _u2 = new UlamekZwykly(u2.Licznik * u.Mianownik, u.Mianownik * u2.Mianownik);

            if (_u.Licznik < _u2.Licznik && _u.Mianownik == _u2.Mianownik)
                return true;
            else
                return false;
        }
        private static bool Wiekszy(UlamekZwykly u, UlamekZwykly u2)
        {
            var _u = new UlamekZwykly(u.Licznik * u2.Mianownik, u.Mianownik * u2.Mianownik);
            var _u2 = new UlamekZwykly(u2.Licznik * u.Mianownik, u.Mianownik * u2.Mianownik);

            if (_u.Licznik > _u2.Licznik && _u.Mianownik == _u2.Mianownik)
                return true;
            else
                return false;
        }

        // ToString():
        public override string ToString() => $"{Licznik} / {Mianownik}";

        #endregion

        #region Helpers
        public static int NajwiekszyWspolnyDzielnik(int a, int b)
        {
            while (a != b)
            {
                if (a > b)
                    a -= b;
                else if (b > a)
                    b -= a;
            }
            return a;
        }

        #endregion

    }
}
