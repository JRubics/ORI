﻿using System.Collections.Generic;
using System.Linq;

namespace ComputationalGraph
{
    public class MultiplyNode
    {
        //niz koji sadrzi ulaz i tezinu
        //prvi element je ulaz, a drugi njegova tezina
        public List<double> x;

        public MultiplyNode()
        {
            x = new List<double>();
            x.Add(0.0);
            x.Add(0.0);
        }
        /// <summary>
        /// Mnozenje ulaza sa tezinom
        /// </summary>
        /// <param name="x"></param>
        /// <returns>Proizvod ulaza i tezine</returns>
        public double forward(List<double> x)
        {
            /*
            this.x = x;
            //TODO 3: implementirati forward funkciju za multiply node
            double mul_retVal = 0.0;
            mul_retVal = x[0] * x[1];

            return mul_retVal;
            */
            this.x = x;
            //TODO 3: implementirati forward funkciju za multiply node
            double mul_retVal = 0.0;

            mul_retVal = this.x[0] * this.x[1];

            return mul_retVal;
        }

        /// <summary>
        /// z = x*y 
        /// dz/dx = y => dx = dz*y
        /// dz/dy = x => dy = dz*x
        /// </summary>
        /// <param name="dz">izvod </param>
        /// <returns>[dx, dy]</returns>
        public List<double> backward(double dz)
        {
            /*
            //TODO 4: implementirati backward funkciju za multiply node
            */
            /*
            List<double> retval = new List<double>();
            
            /*
            foreach (double x_item in x)
            {
                retval.Add(dz * x_item); // dz*x[i], objasnjeno u gornjem nadmetodnom komentaru zasto
            }
            */
            /*
            retval.Add(dz * x[0]);
            retval.Add(dz * x[1]);
            return retval;
           */

            
            //TODO 4: implementirati backward funkciju za multiply node
            List<double> retval = new List<double>();
            retval.Add(dz * this.x[1]);
            retval.Add(dz * this.x[0]);

            return retval;
            


        }
    }
}
