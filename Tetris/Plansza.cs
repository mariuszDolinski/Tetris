using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Tetris
{
    class Plansza : Panel
    {
        protected Graphics g;
        protected Pole[,] siatka;
        protected int szerokosc;
        protected int wysokosc;

        public Plansza()
        {
            wysokosc = 10;
            szerokosc = 10;
            this.Size = new Size(szerokosc * Pole.wymiar + 4, wysokosc * Pole.wymiar + 4);
            siatka = new Pole[szerokosc, wysokosc];
            for (int i = 0; i < szerokosc; i++)
                for (int j = 0; j < wysokosc; j++)
                    siatka[i, j] = new Pole(i, j);
            g = CreateGraphics();
            this.Show();
        }

        public Plansza(int a, int b)
        {
            if (a <= 3 || b <= 3) a = b = 10;
            szerokosc = a;
            wysokosc = b;
            this.Size = new System.Drawing.Size(szerokosc * Pole.wymiar + 4, wysokosc * Pole.wymiar + 4);
            siatka = new Pole[szerokosc, wysokosc];
            for (int i = 0; i < szerokosc; i++)
                for (int j = 0; j < wysokosc; j++)
                    siatka[i, j] = new Pole(i, j);
            g = CreateGraphics();
            this.Show();
        }

        public bool sprawdzWymiary(int x, int y)
        {
            if (x < 0 || x >= szerokosc || y < 0 || y >= wysokosc) return false;
            return true;
        }

        public bool rysujPole(int x, int y)
        {
            if(!sprawdzWymiary(x,y)) return false;
            if (siatka[x, y].czyZajete()) return false;
            siatka[x, y].wyswietlPole(g);
            return true;
        }

        public void odswiez()
        {
            for (int i = 0; i < szerokosc; i++)
                for (int j = 0; j < wysokosc; j++)
                {
                    if (siatka[i, j].czyZajete())
                        siatka[i, j].wyswietlPole(g);
                }
        }

        public void reset()
        {
            for (int i = 0; i < szerokosc; i++)
                for (int j = 0; j < wysokosc; j++)
                    siatka[i, j].ukryjPole(g, BackColor);
        }
    }
}
