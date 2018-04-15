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
        public Boolean k1;
        public Boolean k2;
        public static Boolean stanje = false;

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.k1 = k1;
            rez.k2 = k2;
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

            if(lavirint[markI,markJ] == 4 && markI == Main.kutija1.markI && markJ == Main.kutija1.markJ) {
                k1 = true;
            }else if (lavirint[markI, markJ] == 4 && markI == Main.kutija2.markI && markJ == Main.kutija2.markJ) {
                k2 = true;
            }
            if(k1 && k2) {
                stanje = true;
            }

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
            int hash = 0;
            if (k1) {
                hash += 10000;
            }
            if (k2) {
                hash += 20000;
            }
            return hash + 100*markI + markJ;
        }

        public bool isKrajnjeStanje()
        {
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && k1 && k2;
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
