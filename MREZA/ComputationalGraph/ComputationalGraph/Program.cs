using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ComputationalGraph
{
    class Program
    {
        enum FirstLine
        {
            step, type, amount, nameOrig, oldbalanceOrg, newbalanceOrig, nameDest, oldbalanceDest, newbalanceDest, isFraud, isFlaggedFraud
        }
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("@./../../../../../novi.csv");
            
            lines = lines.Skip(1).ToArray( );
            //ulazi
            List<int> typeList = new List<int>( );
            Dictionary<String, int> TypeDict = new Dictionary<String, int>( );
            List<double> oldBalanceOrgList = new List<double>( );
            List<double> newbalanceOrigList = new List<double>( );
            List<double> oldBalanceDestList = new List<double>( );
            List<double> newbalanceDestList = new List<double>( );
            List<double> amountList = new List<double>( );
            //izlaz
             List<int> isFraudList = new List<int>( );


            foreach (string line in lines) {
                string[] attr = line.Split(',');

                String p = attr[(int)FirstLine.type];
                if (TypeDict.ContainsKey(p)) {
                    typeList.Add(TypeDict[p]);
                } else {
                    TypeDict[p] = TypeDict.Count;
                    typeList.Add(TypeDict[p]);
                }
                oldBalanceOrgList.Add(double.Parse(attr[(int)FirstLine.oldbalanceOrg]));
                newbalanceOrigList.Add(double.Parse(attr[(int)FirstLine.newbalanceOrig]));
                oldBalanceDestList.Add(double.Parse(attr[(int)FirstLine.oldbalanceDest]));
                newbalanceDestList.Add(double.Parse(attr[(int)FirstLine.newbalanceDest]));
                amountList.Add(double.Parse(attr[(int)FirstLine.amount]));

                isFraudList.Add(int.Parse(attr[(int)FirstLine.isFraud]));

            }

            //normalizacije
            double max = oldBalanceOrgList.Max( );
            for (int i = 0; i < oldBalanceOrgList.Count; i++) {
                oldBalanceOrgList[i] /= max;
            }

            max = newbalanceOrigList.Max( );
            for (int i = 0; i < newbalanceOrigList.Count; i++) {
                newbalanceOrigList[i] /= max;
            }

            max = oldBalanceDestList.Max( );
            for (int i = 0; i < oldBalanceDestList.Count; i++) {
                oldBalanceDestList[i] /= max;
            }

            max = newbalanceDestList.Max( );
            for (int i = 0; i < newbalanceDestList.Count; i++) {
                newbalanceDestList[i] /= max;
            }

            max = amountList.Max( );
            for (int i = 0; i < amountList.Count; i++) {
                amountList[i] /= max;
            }

            NeuralNetwork network = new NeuralNetwork( );
            network.Add(new NeuralLayer(6, 6, "sigmoid"));
            network.Add(new NeuralLayer(6, 1, "sigmoid"));

            List<List<double>> X = new List<List<double>>( );
            List<List<double>> Y = new List<List<double>>( );

            for (int i = 0; i < oldBalanceOrgList.Count; i++) {
                if (i % 5 != 0) {
                    double[] xTemp = { (double)typeList[i], (double)oldBalanceOrgList[i], (double)newbalanceOrigList[i], (double)oldBalanceDestList[i], (double)newbalanceDestList[i], (double)amountList[i] };
                    X.Add(xTemp.ToList( ));

                    double[] yTemp = { (double)isFraudList[i] };
                    Y.Add(yTemp.ToList( ));
                }
            }

            Console.WriteLine("obuka");
            network.fit(X, Y, 0.1, 0.9, 100);
            Console.WriteLine("done");

            //int x = (int)Math.Ceiling(oldBalanceOrgList.Count * 0.5);
            int x = 0;
            int ok1 = 0;
            int ok0 = 0;
            int uk1 = 0;
            int uk0 = 0;

            for (int i = x; i < oldBalanceDestList.Count; i++) {
                if (i % 5 == 0) {
                    double[] x1 = { typeList[i], oldBalanceOrgList[i], newbalanceOrigList[i], oldBalanceDestList[i], newbalanceDestList[i], amountList[i] };
                    if (isFraudList[i] == 0) {
                        uk0++;
                        if (network.predict(x1.ToList( ))[0] < 0.5) {
                            ok0++;
                        }
                    } else if (isFraudList[i] == 1) {
                        uk1++;
                        if (network.predict(x1.ToList( ))[0] >= 0.5) {
                            ok1++;
                        }
                    }
                }
            }

            Console.WriteLine("OK 0: " + ok0 + "\nUkupno: " + uk0 + "\nProcenat pogodaka:" + 100*((double)ok0 / uk0) + "\n");
            Console.WriteLine("OK 1: " + ok1 + "\nUkupno: " + uk1 + "\nProcenat pogodaka:" + 100 *((double)ok1 / uk1) + "\n");
            Console.WriteLine("OK UkUPNO: " + (ok1+ok0) + "\nUkupno: " + (uk0+uk1) + "\nProcenat pogodaka:" + 100 * ((double)(ok1+ok0) / (uk0+uk1)) + "\n");

            Console.ReadLine( );
        }
    }
}
