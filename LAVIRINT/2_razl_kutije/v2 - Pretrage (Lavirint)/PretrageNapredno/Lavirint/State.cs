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
        public bool kutija1;
        public bool kutija2;

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.kutija1 = kutija1;
            rez.kutija2 = kutija2;
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
                this.kutija1 = true;
            }
            if (lavirint[markI, markJ] == 5 )
            {
                this.kutija2 = true;
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
            int hashValue = 0;
            if (this.kutija1)
                hashValue += 1000;
            if (this.kutija2)
                hashValue += 2000;

            hashValue += 100 * markI + markJ;
            return hashValue;
        }

        public bool isKrajnjeStanje()
        {
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && this.kutija1 && this.kutija2;
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
