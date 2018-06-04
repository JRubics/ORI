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
        public Boolean kp1;
        public Boolean kp2;
        public Boolean kn1;

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.kp1 = this.kp1;
            rez.kp2 = this.kp2;
            rez.kn1 = this.kn1;
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

            if (lavirint[markI, markJ] == 4 && markI == Main.p1.markI && markJ == Main.p1.markJ ) {
                kp1 = true;
            } else if (lavirint[markI, markJ] == 4 && markI == Main.p2.markI && markJ == Main.p2.markJ) {
                kp2 = true;
            } else if (lavirint[markI, markJ] == 5 && markI == Main.n1.markI && markJ == Main.n1.markJ && kp1 && kp2) {
                kn1 = true;
            } 
            
            if(kp1 && kp2 && kn1) {
                addState(markI + 1, markJ + 1, rez);
                addState(markI + 1, markJ - 1, rez);
                addState(markI - 1, markJ + 1, rez);
                addState(markI - 1, markJ - 1, rez);
            } else if (markJ < Main.brojKolona/2) { //za vrste menjas markI
                addState(markI + 2, markJ + 1, rez);

                addState(markI + 2, markJ - 1, rez);

                addState(markI - 2, markJ + 1, rez);

                addState(markI - 2, markJ - 1, rez);

                addState(markI + 1, markJ + 2, rez);

                addState(markI + 1, markJ - 2, rez);

                addState(markI - 1, markJ + 2, rez);

                addState(markI - 1, markJ - 2, rez);
            } else {
                int i = markI + 1;
                addState(i, markJ, rez);

                i = markI - 1;
                addState(i, markJ, rez);

                int j = markJ + 1;
                addState(markI, j, rez);

                j = markJ - 1;
                addState(markI, j, rez);
            }
            return rez;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            if (kp1)
                hash += 1000;
            if (kp2)
                hash += 2000;
            if (kn1)
                hash += 4000;
            return hash + 100 * markI + markJ;
        }

        public bool isKrajnjeStanje()
        {
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && kp1 && kp2 && kn1;
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
