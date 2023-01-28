using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class Pole
    {
        private Rectangle kwadrat;
        private Color kolor;
        private int x, y;
        private bool zajete;
        public const int wymiar = 18;

        public Pole()
        {
            x = y = 0;
            kwadrat = new Rectangle(x * wymiar, y * wymiar, wymiar, wymiar);
            zajete = false;
            kolor = Color.Black;
        }

        public Pole(int _x, int _y)
        {
            x = _x;
            y = _y;
            kwadrat = new Rectangle(x * wymiar, y * wymiar, wymiar, wymiar);
            zajete = false;
            kolor = Color.Black;
        }

        public Pole(int _x, int _y, Color _kolor)
        {
            x = _x;
            y = _y;
            kwadrat = new Rectangle(x * wymiar, y * wymiar, wymiar, wymiar);
            zajete = false;
            kolor = _kolor;
        }

        public bool czyZajete()
        {
            return zajete;
        }

        public Color pobierzKolor()
        {
            return kolor;
        }

        public void ustawKolor(Color k)
        {
            kolor = k;
        }

        public void wyswietlPole(Graphics g)
        {
            SolidBrush p = new SolidBrush(kolor);
            g.FillRectangle(p, kwadrat);
            g.DrawRectangle(Pens.Black, kwadrat);
            zajete = true;
        }

        public void ukryjPole(Graphics g, Color k)
        {
            SolidBrush p = new SolidBrush(k);
            g.FillRectangle(p, kwadrat);
            g.DrawRectangle(new Pen(p), kwadrat);
            zajete = false;
        }
    }
}
