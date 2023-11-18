namespace Dynamisches_Subnettieren_V1.IPv4
{
    using System;
    using System.Collections.Generic;

    internal class MethodenIPv4
    {
        public static int[] Bits = new int[]
        {
            128,
            64,
            32,
            16,
            8,
            4,
            2,
            1,
            0,
        };
        public static bool CheckOktett(int Input)
        {
            //Checkt auf den richtigen Bereich der Adressierung
            if (Input >= 0 && Input < 256)
                return false;
            else
                return true;
        }
        public static int GetBitToAdress(int Hosts)
        {
            //Ermittelt die Betötigten Bits anhand der angegebenen Hostanzahl
            int Count = 1;
            while (true)
            {
                if (Math.Pow(2, Count) < Hosts)
                    Count++;
                else
                    return Count;
            }
        }
        public static Tuple<int, int> GetTargetOktettAndBit(int TempPräfix)
        {
            //Bestimmt das Oktett, welches Netz-/Host-Adresse trennt
            decimal Temp = TempPräfix / 8;
            if (TempPräfix % 8 == 0)
                Temp++;
            return new Tuple<int, int>((int)Math.Floor(Temp), TempPräfix % 8);
        }
        public static int GiveValueOfBit(int Host)
        {
            //Berechnen der Adresse im Oktett durch gesetzte Bits
            return (int)Math.Pow(2, GetBitToAdress(Host));
        }
        public static int GiveStartBitFromOktett(int Oktett)
        {
            //Ermittelt den gesetzten Bit im Oktett
            int Value = Oktett;
            int count = 0;
            for (int i = 0; i < Bits.Length; i++)
            {
                if (Value <=0)
                    break;
                Value -= Bits[i];
                count++;
            }
            return count;
        }
        static string ConvertAdressArrayToString(int[] Array)
        {
            //Setzt die vier Oktetten aus dem Array zu einer Netzadresse zusammen und gibt diese als String wieder aus
            string TempString = "";
            for (int i = 0; i < Array.Length; i++)
            {
                TempString += $"{Array[i]}";
                if (i < 3)
                    TempString += ".";
            }
            return TempString;
        }
        public static List<Tuple<int[]>> GetBrodcast(List<Tuple<int[]>> NetAdress, List<Tuple<string, int>> NameHostFromNetwok)
        {
            List<Tuple<int[]>> Output = new List<Tuple<int[]>>();
            int[] Brodcast = new int[4];
            for (int i = 0; i < NameHostFromNetwok.Count; i++)
            {
                int[] NextNetwork = NetAdress[i + 1].Item1;
                bool TakeBit = true;
                for (int j = 0; j < NextNetwork.Length; j++)
                {
                    if (NextNetwork[j] == NetAdress[i].Item1[j])
                        Brodcast[j] = NextNetwork[j];
                    else if (NextNetwork[j] > NetAdress[i].Item1[j] && TakeBit)
                    {
                        TakeBit = false;
                        Brodcast[j] = NextNetwork[j] - 1;
                    }

                    else
                        Brodcast[j] = 255;
                }
                Output.Add(new Tuple<int[]>(Brodcast));
            }
            return Output;
        }
    }
}
