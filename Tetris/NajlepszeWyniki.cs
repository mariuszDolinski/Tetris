using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Tetris
{
    public partial class NajlepszeWyniki : Form
    {

        string[] najlepsiNazwy;
        int[] najlepsiPunkty;

        public NajlepszeWyniki()
        {
            InitializeComponent();
            tabela.Text = "";
            najlepsiNazwy = new String[10];
            najlepsiPunkty = new int[10];
            pobierzZPliku();
            for (int i = 0; i < 10; i++)
            {
                if (i < 9)
                {
                    tabela.Text += (i + 1).ToString() + ".   " + najlepsiNazwy[i] + Environment.NewLine;
                    punkty.Text += najlepsiPunkty[i].ToString() + " pkt.\n";
                }
                else
                {
                    tabela.Text += (i + 1).ToString() + ". " + najlepsiNazwy[i] + Environment.NewLine;
                    punkty.Text += najlepsiPunkty[i].ToString() + " pkt.\n";
                }
            }
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

        private void NajlepszeWyniki_Load(object sender, EventArgs e)
        {

        }
    }
}
