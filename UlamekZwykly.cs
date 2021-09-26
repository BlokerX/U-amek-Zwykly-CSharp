using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operators
{
#pragma warning disable CS0660 // Typ definiuje operator == lub !=, ale nie przesłania metody Object.Equals(object o)
#pragma warning disable CS0661 // Typ definiuje operator == lub !=, ale nie przesłania metody Object.GetHashCode()
    public class UlamekZwykly
#pragma warning restore CS0661 // Typ definiuje operator == lub !=, ale nie przesłania metody Object.GetHashCode()
#pragma warning restore CS0660 // Typ definiuje operator == lub !=, ale nie przesłania metody Object.Equals(object o)
    {
        public UlamekZwykly(int licznik, int mianownik)
        {
            if (mianownik == 0)
                throw MianownikToZero;
            Licznik = licznik;
            Mianownik = mianownik;
        }

        public int Licznik { get; set; } = 1;
        public int Mianownik { get { return mianownik; } set { if (value == 0) throw MianownikToZero; mianownik = value; } }
        private int mianownik = 1;
        public float WartoscCalkowita() { return Licznik / Mianownik; }

        /// <summary>
        /// Wyjątek występuje gdy mianownik osiągnął wartość 0
        /// </summary>
        public static Exception MianownikToZero = new Exception("Mianownik wynosi zero");

        #region Skracanie ułamka

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

        #endregion

        #region Operatory_i_przeciążenia

        // Operacje na ułamku:

        public static UlamekZwykly operator +(UlamekZwykly a)
            => a;
        public static UlamekZwykly operator -(UlamekZwykly a)
            => new UlamekZwykly(-a.Licznik, a.Mianownik);


        // Działania na ułamkach i liczbach typu int:

        public static UlamekZwykly operator +(UlamekZwykly u, int i)
            => Skroc(u + new UlamekZwykly(i, 1));
        public static UlamekZwykly operator -(UlamekZwykly u, int i)
            => Skroc(u - new UlamekZwykly(i, 1));
        public static UlamekZwykly operator *(UlamekZwykly u, int i)
            => Skroc(u * new UlamekZwykly(i, 1));
        public static UlamekZwykly operator /(UlamekZwykly u, int i)
            => Skroc(u / new UlamekZwykly(i, 1));

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

        public static UlamekZwykly UlamekZInta(int i)
        {
            return new UlamekZwykly(i, 1);
        }

        public static UlamekZwykly UlamekZFloat(float f)
        {
            int dzielnik = 1;

            while (f % 1 != 0)
            {
                f = f * 10;
                dzielnik = dzielnik * 10;
            }

            return new UlamekZwykly((int)f, dzielnik);
        }
        
        public static UlamekZwykly UlamekZDouble(double d)
        {
            int dzielnik = 1;

            while (d % 1 != 0)
            {
                d = d * 10;
                dzielnik = dzielnik * 10;
            }

            return new UlamekZwykly((int)d, dzielnik);
        }

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
