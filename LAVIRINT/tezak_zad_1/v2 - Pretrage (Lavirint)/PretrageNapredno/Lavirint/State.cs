using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class State
    {
        public static int[,] lavirint;
        State parent;
        public int markI, markJ; //vrsta i kolona
        public double cost;
        public bool p1;
        //public bool p2;
        //public bool p3;
        //public bool n1;
        //public bool n2;

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.p1 = p1;
            //rez.p2 = p2;
            //rez.p3 = p3;
            //rez.n1 = n1;
            //rez.n2 = n2;
            return rez;
        }

        private void addState(int i, int j, List<State> rez)
        {
            if (i >= 0 && i < Main.brojVrsta && j < Main.brojKolona && j >= 0)
            {
                State newState = sledeceStanje(i, j);
                if (lavirint[i, j] == 1)
                {
                    return;
                }
                rez.Add(newState);
            }
        }

        public List<State> mogucaSledecaStanja()
        {
            //TODO1: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu
            //TODO2: Prosiriti metodu tako da se ne moze prolaziti kroz sive kutije
            List<State> rez = new List<State>();

            if (lavirint[markI, markJ] == 4)
            {
                if (markI == Main.plava1.markI && markJ == Main.plava1.markJ)
                {
                    p1 = true;
                }
                /*else if (markI == Main.plava2.markI && markJ == Main.plava2.markJ)
                {
                    p2 = true;
                }
                else if (markI == Main.plava3.markI && markJ == Main.plava3.markJ)
                {
                    p3 = true;
                }*/
            }
            /*else if (lavirint[markI, markJ] == 5) {
                if (markI == Main.narandzasta1.markI && markJ == Main.narandzasta1.markJ)
                {
                    n1 = true;
                }
                else if (markI == Main.narandzasta2.markI && markJ == Main.narandzasta2.markJ)
                {
                    n2 = true;
                }
        }*/

            int i = markI + 1;
            addState(i, markJ, rez);

            i = markI - 1;
            addState(i, markJ, rez);

            int j = markJ + 1;
            addState(markI, j, rez);

            j = markJ - 1;
            addState(markI, j, rez);
            return rez;
        }

        public override int GetHashCode()
        {
            int hashValue = 0;
            if (this.p1)
                hashValue += 1000;
            hashValue = 100* markI + markJ;
            return hashValue;
        }

        public bool isKrajnjeStanje()
        {
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && p1;
        }

        public List<State> path()
        {
            List<State> putanja = new List<State>();
            State tt = this;
            while (tt != null)
            {
                putanja.Insert(0, tt);
                tt = tt.parent;
            }
            return putanja;
        }

        
    }
}
