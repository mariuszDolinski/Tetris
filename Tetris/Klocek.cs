using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class Klocek
    {
        public enum kolory { niebieski, zolty, czerwony, brazowy, zielony };
        public enum klocki { L, odw_L, palka, kwadrat, trojkat, piorun, odw_piorun };

        public bool[,] siatka;
        public klocki typ;
        public Color kolor;
        public int kat;
        private static Random losuj = new Random();

        public Klocek()
        {
            kat = 0;
            siatka = new bool[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    siatka[i, j] = false;

            kolory nowyKolor = (kolory)losuj.Next(4);
            switch (nowyKolor)
            {
                case kolory.niebieski: kolor = Color.LightBlue; break;
                case kolory.zolty: kolor = Color.LemonChiffon; break;
                case kolory.brazowy: kolor = Color.BurlyWood; break;
                case kolory.czerwony: kolor = Color.Coral; break;
                case kolory.zielony: kolor = Color.YellowGreen; break;

                default: kolor = Color.Black; break;
            }

            klocki nowyKlocek = (klocki)losuj.Next(6);
            switch (nowyKlocek)
            {
                case klocki.L:
                    siatka[0, 0] = siatka[0, 1] = siatka[0, 2] = siatka[1, 2] = true;
                    typ = klocki.L;
                    break;
                case klocki.odw_L: 
                    siatka[1, 0] = siatka[1, 1] = siatka[1, 2] = siatka[0, 2] = true;
                    typ = klocki.odw_L;
                    break;
                case klocki.palka: siatka[0, 0] = siatka[0, 1] = siatka[0, 2] = siatka[0, 3] = true;
                    typ = klocki.palka;
                    break;
                case klocki.kwadrat: siatka[0, 0] = siatka[1, 0] = siatka[0, 1] = siatka[1, 1] = true;
                    typ = klocki.kwadrat;
                    break;
                case klocki.trojkat: siatka[1, 0] = siatka[0, 1] = siatka[1, 1] = siatka[2, 1] = true;
                    typ = klocki.trojkat;
                    break;
                case klocki.piorun: siatka[0, 0] = siatka[0, 1] = siatka[1, 1] = siatka[1, 2] = true;
                    typ = klocki.piorun;
                    break;
                case klocki.odw_piorun: siatka[1, 0] = siatka[1, 1] = siatka[0, 1] = siatka[0, 2] = true;
                    typ = klocki.odw_piorun;
                    break;
            }
        }

        public Klocek odwrocKlocek(int fi)
        {
            if (fi != 0 && fi != 90 && fi != 180 && fi != 270) return this;
            Klocek nowy = new Klocek();
            nowy.kat = fi;
            nowy.kolor = this.kolor;
            nowy.typ = this.typ;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    nowy.siatka[i, j] = false;
            switch (this.typ)
            {
                case klocki.L:
                    switch (fi)
                    {
                        case 0: nowy.siatka[0, 0] = nowy.siatka[0, 1] = nowy.siatka[0, 2] = nowy.siatka[1, 2] = true; break;
                        case 90: nowy.siatka[0, 0] = nowy.siatka[1, 0] = nowy.siatka[2, 0] = nowy.siatka[0, 1] = true; break;
                        case 180: nowy.siatka[0, 0] = nowy.siatka[1, 0] = nowy.siatka[1, 1] = nowy.siatka[1, 2] = true; break;
                        case 270: nowy.siatka[2, 0] = nowy.siatka[0, 1] = nowy.siatka[1, 1] = nowy.siatka[2, 1] = true; break;
                    }
                    break;
                case klocki.odw_L:
                    switch (fi)
                    {
                        case 0: nowy.siatka[1, 0] = nowy.siatka[1, 1] = nowy.siatka[1, 2] = nowy.siatka[0, 2] = true; break;
                        case 90: nowy.siatka[0, 0] = nowy.siatka[0, 1] = nowy.siatka[1, 1] = nowy.siatka[2, 1] = true; break;
                        case 180: nowy.siatka[0, 0] = nowy.siatka[0, 1] = nowy.siatka[0, 2] = nowy.siatka[1, 0] = true; break;
                        case 270: nowy.siatka[0, 0] = nowy.siatka[1, 0] = nowy.siatka[2, 0] = nowy.siatka[2, 1] = true; break;
                    }
                    break;
                case klocki.palka:
                    switch (fi)
                    {
                        case 0:
                        case 180:
                            nowy.siatka[0, 0] = nowy.siatka[0, 1] = nowy.siatka[0, 2] = nowy.siatka[0, 3] = true; break;
                        case 90:
                        case 270:
                            nowy.siatka[0, 0] = nowy.siatka[1, 0] = nowy.siatka[2, 0] = nowy.siatka[3, 0] = true; break;
                    }
                    break;
                case klocki.kwadrat:
                    nowy.siatka[0, 0] = nowy.siatka[1, 0] = nowy.siatka[0, 1] = nowy.siatka[1, 1] = true; break;
                case klocki.trojkat:
                    switch (fi)
                    {
                        case 0: nowy.siatka[1, 0] = nowy.siatka[0, 1] = nowy.siatka[1, 1] = nowy.siatka[2, 1] = true; break;
                        case 90: nowy.siatka[0, 0] = nowy.siatka[0, 1] = nowy.siatka[0, 2] = nowy.siatka[1, 1] = true; break;
                        case 180: nowy.siatka[0, 0] = nowy.siatka[1, 0] = nowy.siatka[2, 0] = nowy.siatka[1, 1] = true; break;
                        case 270: nowy.siatka[1, 0] = nowy.siatka[1, 1] = nowy.siatka[1, 2] = nowy.siatka[0, 1] = true; break;
                    }
                    break;
                case klocki.piorun:
                    switch (fi)
                    {
                        case 0:
                        case 180:
                            nowy.siatka[0, 0] = nowy.siatka[0, 1] = nowy.siatka[1, 1] = nowy.siatka[1, 2] = true; break;
                        case 90:
                        case 270:
                            nowy.siatka[1, 0] = nowy.siatka[2, 0] = nowy.siatka[0, 1] = nowy.siatka[1, 1] = true; break;
                    }
                    break;
                case klocki.odw_piorun:
                    switch (fi)
                    {
                        case 0:
                        case 180:
                            nowy.siatka[1, 0] = nowy.siatka[1, 1] = nowy.siatka[0, 1] = nowy.siatka[0, 2] = true; break;
                        case 90:
                        case 270:
                            nowy.siatka[0, 0] = nowy.siatka[1, 0] = nowy.siatka[1, 1] = nowy.siatka[2, 1] = true; break;
                    }
                    break;
            }

            return nowy;
        }
    }
}
