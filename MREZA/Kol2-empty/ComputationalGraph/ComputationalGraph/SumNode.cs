﻿using System.Collections.Generic;
using System.Linq;

namespace ComputationalGraph
{
    public class SumNode
    {
        private List<double> x;

        public SumNode()
        {
            x = new List<double>();
        }
        /// <summary>
        /// Suma svih elemenata
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double forward(List<double> x)
        {
            /*
            this.x = x;
            //TODO 1: implementirati forward funckiju za sum node
            double sum_retVal = 0.0;
            foreach (double x_value in x)
            {
                sum_retVal += x_value;
            }

            return sum_retVal;
            */
            this.x = x;
            //TODO 1: implementirati forward funckiju za sum node
            double sum_retVal = 0.0;
            foreach (double val in x)
            {
                sum_retVal += val;
            }
            return sum_retVal;
        }
        /// <summary>
        /// Izvod funkcije po svakom elementu
        /// z = x + y 
        /// dz/dx = 1 => dx = dz
        /// dz/dy = 1 => dy = dz
        /// </summary>
        /// <param name="dz"></param>
        /// <returns></returns>
        public List<double> backward(double dz)
        {
            /*
            //TODO 2: implementirati backward funkciju za sum node
            List<double> retVal = new List<double>();
            foreach (double x_item in x)
            {
                retVal.Add(dz); //za svaku promenljivu x vektora dodeliti da je jednak dx[i] = dz
                                //to se tako vrsi zato sto nam treba suma
            }
            return retVal;
            */
            //TODO 2: implementirati backward funkciju za sum node
            List<double> retVal = new List<double>();
            foreach (double val in x)
            {
                retVal.Add(dz * 1);
            }
            return retVal;
        }
    }
}
