﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Lavirint
{
    class AStarSearch
    {
        public State search(State pocetnoStanje)
        {
            List<State> stanjaZaObradu = new List<State>();
            Hashtable predjeniPut = new Hashtable();
            stanjaZaObradu.Add(pocetnoStanje);

            while (stanjaZaObradu.Count > 0)
            {
                State naObradi = getBest(stanjaZaObradu);

                if (!predjeniPut.ContainsKey(naObradi.GetHashCode()))
                {
                    Main.allSearchStates.Add(naObradi);
                    if (naObradi.isKrajnjeStanje())
                    {
                        return naObradi;
                    }
                    predjeniPut.Add(naObradi.GetHashCode(), null);
                    List<State> sledecaStanja = naObradi.mogucaSledecaStanja();

                    foreach (State s in sledecaStanja)
                    {
                        stanjaZaObradu.Add(s);
                    }
                }
                stanjaZaObradu.Remove(naObradi);
            }
            return null;
        }

        //funkcija odredjuje rastojanje
        public double heuristicFunction(State s)
        {
            //jedna
            //return Math.Sqrt(Math.Pow(s.markI - Main.krajnjeStanje.markI, 2) + Math.Pow(s.markJ - Main.krajnjeStanje.markJ, 2)) + s.cost - Math.Sqrt(Math.Pow(s.markI - Main.vatra.markI, 2) + Math.Pow(s.markJ - Main.vatra.markJ, 2));

            //vise
            double x = Math.Sqrt(Math.Pow(s.markI - Main.krajnjeStanje.markI, 2) + Math.Pow(s.markJ - Main.krajnjeStanje.markJ, 2)) + s.cost;
            double num = Main.vatre.Capacity;
            foreach(State v in Main.vatre) {
                x -= 1 / num * Math.Sqrt(Math.Pow(s.markI - v.markI, 2) + Math.Pow(s.markJ - v.markJ, 2));
            }
            return x;
        }

        public State getBest(List<State> stanja)
        {
            State rez = null;
            double min = Double.MaxValue;

            foreach (State s in stanja)
            {
                double h = heuristicFunction(s);
                if (h < min)
                {
                    min = h;
                    rez = s;
                }
            }
            return rez;
        }



    }
}
