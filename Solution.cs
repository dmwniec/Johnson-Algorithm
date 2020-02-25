using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dzonson
{
    class Solution
    {
        public static int[] Johnson(Matrix matrix)
        {
            
            
            int Amount = matrix.Value.GetLength(0);
            int[] Jobs = new int[Amount];
            int[] MachineA = new int[Amount];
            int[] MachineB = new int[Amount];
            for (int i = 0; i<Amount; i++)
            {
                MachineA[i] = matrix.Value[i, 0];
                MachineB[i] = matrix.Value[i, 1];
            }
            int BackOfSequence = Amount - 1;
            int StartOfSequence = 0;
            for (int i = 0; i < Amount; i++)
            {
                int MinofA = MachineA.Min();
                int MinofB = MachineB.Min();
                int index;
              
                if (MinofA <= MinofB )
                {
                 
                    index = Array.IndexOf(MachineA, MinofA);
                    Jobs[StartOfSequence] = index;
                    MachineA[index] = 100;
                    MachineB[index] = 100;
                    StartOfSequence++;
                }
                
                else
                {
                   
                    index = Array.IndexOf(MachineB, MinofB);
                    Jobs[BackOfSequence] = index;
                    MachineA[index] = 100;
                    MachineB[index] = 100;
                    BackOfSequence--;
                }

                    

            }
            
                return Jobs;
        }
        public static int TimeCalc(Matrix matrix, int[] JobSequence)
        {
            int time = 0;
            int Amount = matrix.Value.GetLength(0);
            ArrayList MachineA = new ArrayList();
            ArrayList MachineB = new ArrayList();
           
            for (int i = 0; i<Amount;i++)
            {
                int LengthA =  matrix.Value[JobSequence[i], 0];
                int LengthB = matrix.Value[JobSequence[i], 1];
                for (int j = 0; j < LengthA; j++) MachineA.Add(0);
                while(MachineA.Count > MachineB.Count)
                {
                    MachineB.Add(0);
                }
                for (int j = 0; j < LengthB; j++) MachineB.Add(0);

            }
            time = MachineB.Count;
            
             return time;
        }

    }
}
