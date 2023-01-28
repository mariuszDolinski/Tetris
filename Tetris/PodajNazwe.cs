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
    public partial class PodajNazwe : Form
    {
        int p;
        public static string nazwa;
        public PodajNazwe()
        {
            InitializeComponent();
            label1.Text = "Gratulacje. Dostałeś się na listę najlepszych" + Environment.NewLine + "Podaj swoje imię:";
        }

        public PodajNazwe(int _p)
        {
            InitializeComponent();
            p = _p;
            label1.Text = "Gratulacje. Dostałeś się na listę najlepszych" + Environment.NewLine + "Podaj swoje imię:";
        }

        private void zapiszDoPliku()
        {
            FileStream plik = new FileStream("najlepsi.txt", FileMode.Truncate);
            StreamWriter zapisz = new StreamWriter(plik);
            for (int i = 0; i < 10; i++)
            {
                zapisz.WriteLine(Form1.najlepsiNazwy[i]);
                zapisz.WriteLine(Form1.najlepsiPunkty[i].ToString());
            }
            zapisz.Close();
        }

        private void PodajNazwe_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "") 
            {
                Form1.najlepsiNazwy[p] = textBox1.Text;
                zapiszDoPliku();
                this.Close();
            }
        }
    }
}
