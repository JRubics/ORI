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
        public Boolean kp3;
        public Boolean kn1;
        public Boolean kn2;
        public Boolean plava = true;

        public State sledeceStanje(int markI, int markJ)
        {
            State rez = new State();
            rez.markI = markI;
            rez.markJ = markJ;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.kp1 = this.kp1;
            rez.kp2 = this.kp2;
            rez.kp3 = this.kp3;
            rez.kn1 = this.kn1;
            rez.kn2 = this.kn2;
            rez.plava = this.plava;
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

            //skinuti && plava i staviti da mroa sve tri plave ako oces plava pa narandzasta
            if (lavirint[markI, markJ] == 4 && markI == Main.plava1.markI && markJ == Main.plava1.markJ && plava) {
                kp1 = true;
                plava = false;
            } else if(lavirint[markI, markJ] == 4 && markI == Main.plava2.markI && markJ == Main.plava2.markJ && plava) {
                kp2 = true;
                plava = false;
            } else if (lavirint[markI, markJ] == 4 && markI == Main.plava3.markI && markJ == Main.plava3.markJ && plava) {
                kp3 = true;
                plava = false;
            }

            if (lavirint[markI, markJ] == 5 && markI == Main.narandzasta1.markI && markJ == Main.narandzasta1.markJ && !plava) {
                kn1 = true;
                plava = true;
            } else if (lavirint[markI, markJ] == 5 && markI == Main.narandzasta2.markI && markJ == Main.narandzasta2.markJ && !plava) {
                kn2 = true;
                plava = true;
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
            if (kp1)
                hash += 1000;
            if (kp2)
                hash += 2000;
            if (kp3)
                hash += 4000;
            if (kn1)
                hash += 8000;
            if (kn2)
                hash += 16000;
            return hash+100*markI + markJ;
        }

        public bool isKrajnjeStanje()
        {
            return Main.krajnjeStanje.markI == markI && Main.krajnjeStanje.markJ == markJ && kp1 && kp2 && kp3 && kn1 && kn2;
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
