using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    class MenedzerGry : Plansza
    {
        private int xk, yk; //wsp. lewo górnego wierzchołka klocka
        private Klocek klocek;

        public MenedzerGry()
        {
            xk = yk = 0;
        }

        public MenedzerGry(int a, int b)
            : base(a, b)
        {
            xk = yk = 0;
        }

        public bool rysujKlocek(int x, int y, Klocek k)
        {
            if (!sprawdzWymiary(x, y)) return false;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if(k.siatka[i, j])
                    {
                        if (!sprawdzWymiary(x + i, y + j)) return false;
                        if (siatka[x + i, y + j].czyZajete()) return false;
                    }
            xk = x;
            yk = y;
            if (klocek != k) klocek = k;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (k.siatka[i, j])
                    {
                        siatka[x + i, y + j].ustawKolor(k.kolor);
                        rysujPole(x + i, y + j);
                    }
            return true;
        }

        public bool przesunWDol()
        {
            if (klocek == null) return false;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (klocek.siatka[i, j] && (j == 3 || !klocek.siatka[i, j + 1]))
                    {
                        if (!sprawdzWymiary(xk + i, yk + j + 1)) return false;
                        if (siatka[xk + i, yk + j + 1].czyZajete()) return false;
                    }
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (klocek.siatka[i, j])
                        siatka[xk + i, yk + j].ukryjPole(g, BackColor);
            rysujKlocek(xk, yk + 1, klocek);
            return true;
        }

        public bool przesunWLewo()
        {
            if (klocek == null) return false;
            int i, j;
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    if (klocek.siatka[i, j] && (i == 0 || !klocek.siatka[i - 1, j]))
                    {
                        if (!sprawdzWymiary(xk + i - 1, yk + j)) return false;
                        if (siatka[xk + i - 1, yk + j].czyZajete()) return false;
                    }
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    if (klocek.siatka[i, j]) 
                        siatka[xk + i, yk + j].ukryjPole(g, BackColor);
            rysujKlocek(xk - 1, yk, klocek);
            return true;
        }

        public bool przesunWPrawo()
        {
            if (klocek == null) return false;
            int i, j;
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    if (klocek.siatka[i, j] && (i == 3 || !klocek.siatka[i + 1, j]))
                    {
                        if (!sprawdzWymiary(xk + i + 1, yk + j)) return false;
                        if (siatka[xk + i + 1, yk + j].czyZajete()) return false;
                    }
            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    if (klocek.siatka[i, j]) siatka[xk + i, yk + j].ukryjPole(g, BackColor);
            rysujKlocek(xk + 1, yk, klocek);
            return true;
        }

        public void obrocWLewo()
        {
            if (klocek.kat == 0) klocek.kat = 270;
            else klocek.kat -= 90;
            Klocek nowy = klocek.odwrocKlocek(klocek.kat);
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (nowy.siatka[i, j] && !sprawdzWymiary(xk + i, yk + j)) return;
                    if (nowy.siatka[i, j] && !klocek.siatka[i, j] && siatka[xk + i, yk + j].czyZajete()) return;
                }
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (klocek.siatka[i, j]) siatka[xk + i, yk + j].ukryjPole(g, BackColor);
            rysujKlocek(xk, yk, nowy);
        }

        public void obrocWPrawo()
        {
            if (klocek.kat == 270) klocek.kat = 0;
            else klocek.kat += 90;
            Klocek nowy = klocek.odwrocKlocek(klocek.kat);
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (nowy.siatka[i, j] && !sprawdzWymiary(xk + i, yk + j)) return;
                    if (nowy.siatka[i, j] && !klocek.siatka[i, j] && siatka[xk + i, yk + j].czyZajete()) return;
                }
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (klocek.siatka[i, j]) siatka[xk + i, yk + j].ukryjPole(g, BackColor);
            rysujKlocek(xk, yk, nowy);
        }

        public void wyczyscLinie(int nr)
        {
            if (!sprawdzWymiary(1, nr)) return;
            for (int i = 0; i < szerokosc; i++)
                siatka[i, nr].ukryjPole(g, BackColor);
        }

        public void przesunLinie(int nr)
        {
            if (nr < 0 || nr >= wysokosc - 1) return;
            for (int i = 0; i < szerokosc; i++)
            {
                if (!siatka[i, nr].czyZajete())
                {
                    siatka[i, nr + 1].ukryjPole(g, BackColor);
                    continue;
                }
                siatka[i, nr + 1].ustawKolor(siatka[i, nr].pobierzKolor());
                siatka[i, nr + 1].wyswietlPole(g);
                siatka[i, nr].ukryjPole(g, BackColor);
            }
        }

        public int linie()
        {
            int n = wysokosc - 1;
            int ile = 0;
            bool czyLinia = true;
            while (n > 0 && ile < 4)
            {
                for (int i = 0; i < szerokosc; i++)
                    if (!siatka[i, n].czyZajete()) czyLinia = false;
                if (czyLinia)
                {
                    ile++;
                    for (int m = n - 1; m >= 0; m--)
                        przesunLinie(m);
                }
                else
                {
                    n--;
                }
                czyLinia = true;    
            }
            return ile;
        }

        public void zamienNaSzare()
        {
            for(int i=0; i<szerokosc; i++)
                for(int j=0; j<wysokosc; j++)
                    if (siatka[i, j].czyZajete())
                    {
                        siatka[i, j].ustawKolor(System.Drawing.Color.DarkGray);
                    }
        }

        public Klocek pobierzKlocek()
        {
            return klocek;
        }

    }
}
