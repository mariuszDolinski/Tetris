using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

///////////////////////////////////////////////
///         TETRIS GAME
///         Author: Mariusz Doliński
///////////////////////////////////////////////

namespace Tetris
{
    public partial class Form1 : Form
    {
        private MenedzerGry plansza;
        private MenedzerGry nastepny;

        private Label wynik1, wynik2;
        private Label poziom1, poziom2;
        private Label pomoc;

        public int punkty;
        public int poziom;

        private bool szybka; //tryb szybki (po wcisnięciu strzałki w dół)
        private bool gra;
        private bool pauza;

        public static string[] najlepsiNazwy;
        public static int[] najlepsiPunkty;
        public static string nazwa;

        public Form1()
        {
            InitializeComponent();
            inicjujGre();
            najlepsiNazwy = new String[10];
            najlepsiPunkty = new int[10];
            pobierzZPliku();
            zapiszDoPliku();
        }

        private void pobierzZPliku()
        {
            string linia;
            FileStream plik = new FileStream("najlepsi.txt", FileMode.Open);
            StreamReader czytaj = new StreamReader(plik);
            for (int i = 0; i < 20; i++)
            {
                linia = czytaj.ReadLine();
                if (i % 2 == 0)
                {
                    najlepsiNazwy[i / 2] = linia;
                }
                else
                {
                    najlepsiPunkty[i / 2] = int.Parse(linia);
                }
            }
            czytaj.Close();
        }

        private void zapiszDoPliku()
        {
            FileStream plik = new FileStream("najlepsi.txt", FileMode.Truncate);
            StreamWriter zapisz = new StreamWriter(plik);
            for (int i = 0; i < 10; i++)
            {
                zapisz.WriteLine(najlepsiNazwy[i]);
                zapisz.WriteLine(najlepsiPunkty[i].ToString());
            }
            zapisz.Close();
        }

        private void inicjujGre()
        {
            punkty = 0;
            poziom = 1;
            gra = false;
            pauza = false;
            szybka = false;

            licznik.Interval = 1000;

            this.plansza = new MenedzerGry(10, 18);
            this.nastepny = new MenedzerGry(4, 4);

            this.plansza.Location = new System.Drawing.Point(15, 39);
            this.plansza.BorderStyle = BorderStyle.FixedSingle;
            this.plansza.Name = "PlanszaDoGry";
            this.plansza.TabIndex = 0;
            this.plansza.Paint += new PaintEventHandler(this.plansza_Paint);

            this.nastepny.Location = new System.Drawing.Point(30 + plansza.Width, 39);
            this.nastepny.BorderStyle = BorderStyle.Fixed3D;
            this.nastepny.Name = "nastepny";
            this.nastepny.TabIndex = 0;
            this.nastepny.Paint += new PaintEventHandler(this.plansza_Paint);

            int pom = 40 + plansza.Width + nastepny.Width;

            this.wynik1 = new Label();
            this.poziom1 = new Label();
            this.wynik2 = new Label();
            this.poziom2 = new Label();
            this.pomoc = new Label();

            this.wynik1.Location = new System.Drawing.Point(pom, 39);
            this.wynik1.Width = 85;
            this.wynik1.Height = (nastepny.Height - 3) / 4;
            this.wynik1.BorderStyle = BorderStyle.Fixed3D;
            this.wynik1.Text = "Punkty";
            this.wynik1.TextAlign = ContentAlignment.MiddleCenter;
            this.wynik1.BackColor = Color.LightGreen;

            this.wynik2.Location = new System.Drawing.Point(pom, 40 + wynik1.Height);
            this.wynik2.Width = 85;
            this.wynik2.Height = (nastepny.Height - 3) / 4;
            this.wynik2.BorderStyle = BorderStyle.Fixed3D;
            this.wynik2.Text = "0";
            this.wynik2.TextAlign = ContentAlignment.MiddleCenter;
            this.wynik2.BackColor = Color.LightGreen;
            
            this.poziom1.Location = new System.Drawing.Point(pom, 41 + 2*wynik1.Height);
            this.poziom1.Width = 85;
            this.poziom1.Height = (nastepny.Height - 3) / 4;
            this.poziom1.BorderStyle = BorderStyle.Fixed3D;
            this.poziom1.Text = "Poziom";
            this.poziom1.TextAlign = ContentAlignment.MiddleCenter;
            this.poziom1.BackColor = Color.LightBlue;

            this.poziom2.Location = new System.Drawing.Point(pom, 42 + 3 * wynik1.Height);
            this.poziom2.Width = 85;
            this.poziom2.Height = (nastepny.Height - 3) / 4;
            this.poziom2.BorderStyle = BorderStyle.Fixed3D;
            this.poziom2.Text = "1";
            this.poziom2.TextAlign = ContentAlignment.MiddleCenter;
            this.poziom2.BackColor = Color.LightBlue;

            this.pomoc.Location = new System.Drawing.Point(30 + plansza.Width, 54 + nastepny.Height);
            this.pomoc.Width = this.nastepny.Width + 10 + wynik1.Width;
            this.pomoc.Height = this.plansza.Height - this.nastepny.Height - 15;
            this.pomoc.BorderStyle = BorderStyle.FixedSingle;
            this.pomoc.Text = Environment.NewLine + "          KLAWISZOLOGIA" + Environment.NewLine + Environment.NewLine
                + "Nowa gra - F2" + Environment.NewLine
                + "Koniec gry - F3" + Environment.NewLine + Environment.NewLine
                + "Pauza - Spacja" + Environment.NewLine + Environment.NewLine
                + "Przesun w prawo/lewo - \n- strzałka w prawo/lewo" + Environment.NewLine + Environment.NewLine
                + "Przyspieszenie - strzałka w dół" + Environment.NewLine + Environment.NewLine
                + "Obrót w prawo - Z" + Environment.NewLine
                + "Obrót w lewo - C" + Environment.NewLine + Environment.NewLine
                + "Najlepsze Wyniki - X";

            this.Width = 145 + plansza.Width + nastepny.Width;
            this.Height = 79 + plansza.Height;
            this.Controls.Add(this.nastepny);
            this.Controls.Add(this.plansza);
            this.Controls.Add(this.wynik1);
            this.Controls.Add(this.wynik2);
            this.Controls.Add(this.poziom1);
            this.Controls.Add(this.poziom2);
            this.Controls.Add(this.pomoc);
        }

        private bool dodajNajlepszyWynik(int w)
        {
            int p = -1;
            for (int i = 0; i < 10; i++)
                if (w > najlepsiPunkty[i])
                {
                    p = i;
                    break;
                }
            if (p == -1) return false;
            else
            {
                PodajNazwe okno = new PodajNazwe(p);
                okno.Show();
                for (int i = 9; i > p; i--)
                {
                    najlepsiNazwy[i] = najlepsiNazwy[i - 1];
                    najlepsiPunkty[i] = najlepsiPunkty[i - 1];
                }
                najlepsiPunkty[p] = punkty;
                zapiszDoPliku();
            }
            return true;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!plansza.przesunWDol()) //co jeśli klocka nie da się przesunąć w dół
            {
                plansza.zamienNaSzare(); //zamieniamy kolor na szary
                if (szybka) // jeśli bylismy w trybie szybkim, to wracamy do aktualnej prędkości
                {
                    szybka = false;
                    if (poziom < 11)
                        licznik.Interval = 1000 - 100 * (poziom - 1);
                    else if (poziom < 21)
                        licznik.Interval = 100 - 10 * (poziom - 1);
                    else licznik.Interval = 10;
                }
                int pkt = plansza.linie(); // sprawdzamy czy są poziome linie do wyczyszczenia
                punkty += (pkt * pkt);
                wynik2.Text = punkty.ToString();
                if(punkty >= poziom * 10)
                {
                    poziom++;
                    poziom2.Text = poziom.ToString();
                    if (poziom < 11)
                        licznik.Interval -= 100;
                    else if (poziom < 21)
                        licznik.Interval -= 10;
                    else licznik.Interval = 10;
                }
                Klocek k = nastepny.pobierzKlocek();
                if (!plansza.rysujKlocek(5, 0, k)) 
                {
                    licznik.Stop();
                    licznik.Interval = 1000;
                    gra = false;
                    k = null;
                    plansza.reset();
                    nastepny.reset();
                    if (!dodajNajlepszyWynik(punkty))
                        if (punkty == 1)
                        {
                            MessageBox.Show("Zdobyłeś " + punkty.ToString() + " punkt.", "Koniec gry");
                        }
                        else if (punkty % 10 == 2 || punkty % 10 == 3 || punkty % 10 == 4)
                        {
                            MessageBox.Show("Zdobyłeś " + punkty.ToString() + " punkty.", "Koniec gry");
                        }
                        else
                        {
                            MessageBox.Show("Zdobyłeś " + punkty.ToString() + " punktów.", "Koniec gry");
                        }
                    poziom = 1;
                    punkty = 0;
                    wynik2.Text = "0";
                    poziom2.Text = "1";
                    return;
                }
                nastepny.reset();
                nastepny.rysujKlocek(0, 0, new Klocek());             
            }
            plansza.odswiez();
        }

        private void plansza_Paint(object sender, PaintEventArgs e)
        {
            MenedzerGry p = (MenedzerGry)sender;
            p.odswiez();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && !gra)
            {
                gra = true;
                plansza.rysujKlocek(5, 0, new Klocek());
                nastepny.rysujKlocek(0, 0, new Klocek());
                licznik.Start();
            }
            else if (e.KeyCode == Keys.Left && gra && !pauza)
            {
                plansza.przesunWLewo();
            }
            else if (e.KeyCode == Keys.Right && gra && !pauza)
            {
                plansza.przesunWPrawo();
            }
            else if (e.KeyCode == Keys.Z && gra && !pauza)
            {
                plansza.obrocWLewo();
            }
            else if (e.KeyCode == Keys.C && gra && !pauza)
            {
                plansza.obrocWPrawo();
            }
            else if (e.KeyCode == Keys.F3 && gra)
            {
                gra = false;
                licznik.Stop();
                licznik.Interval = 1000;
                plansza.reset();
                nastepny.reset();
                if (!dodajNajlepszyWynik(punkty))
                    if (punkty == 1)
                    {
                        MessageBox.Show("Zdobyłeś " + punkty.ToString() + " punkt.", "Koniec gry");
                    }
                    else if (punkty % 10 == 2 || punkty % 10 == 3 || punkty % 10 == 4)
                    {
                        MessageBox.Show("Zdobyłeś " + punkty.ToString() + " punkty.", "Koniec gry");
                    }
                    else
                    {
                        MessageBox.Show("Zdobyłeś " + punkty.ToString() + " punktów.", "Koniec gry");
                    }
                poziom = 1;
                punkty = 0;
                wynik2.Text = "0";
                poziom2.Text = "1";
            }
            else if (e.KeyCode == Keys.Space && gra)
            {
                if (pauza)
                {
                    licznik.Start();
                    pauza = false;
                }
                else
                {
                    licznik.Stop();
                    pauza = true;
                }
            }
            else if (e.KeyCode == Keys.Down && gra && !pauza)
            {
                licznik.Interval = 10;
                szybka = true;
            }
            else if (e.KeyCode == Keys.X && (!gra || pauza)) 
            {
                NajlepszeWyniki o = new NajlepszeWyniki();
                o.Show();
            }
        }

        private void Form1_KeyReleased(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && gra && !pauza)
            {
                if (poziom < 11)
                    licznik.Interval = 1000 - 100 * (poziom - 1);
                else if (poziom < 21)
                    licznik.Interval = 100 - 10 * (poziom - 1);
                else licznik.Interval = 10;
                szybka = false;
            }
        }

        private void nowaGraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gra = true;
            plansza.rysujKlocek(5, 0, new Klocek());
            nastepny.rysujKlocek(0, 0, new Klocek());
            licznik.Start();
        }

        private void zakończGręToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gra = false;
            licznik.Stop();
            licznik.Interval = 1000;
            plansza.reset();
            nastepny.reset();
            if (!dodajNajlepszyWynik(punkty))
                if (punkty == 1)
                {
                    MessageBox.Show("Zdobyłeś " + punkty.ToString() + " punkt.", "Koniec gry");
                }
                else if (punkty % 10 == 2 || punkty % 10 == 3 || punkty % 10 == 4)
                {
                    MessageBox.Show("Zdobyłeś " + punkty.ToString() + " punkty.", "Koniec gry");
                }
                else
                {
                    MessageBox.Show("Zdobyłeś " + punkty.ToString() + " punktów.", "Koniec gry");
                }
            poziom = 1;
            punkty = 0;
            wynik2.Text = "0";
            poziom2.Text = "1";
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void najlepszeWynikiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NajlepszeWyniki o = new NajlepszeWyniki();
            o.Show();
        }
    }
}
