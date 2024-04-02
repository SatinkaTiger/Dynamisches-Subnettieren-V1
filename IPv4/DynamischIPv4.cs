namespace Dynamisches_Subnettieren_V1.IPv4
{
    using Dynamisches_Subnettieren_V1.UserInterface;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class DynamischIPv4
    {
        public static int[] NetAdress = new int[4];
        public static List<Tuple<string, int>> SubNetNameAndHosts = new();
        public static List<Tuple<int[], int[], int>> SubNetAdress = new();
        public static void Start()
        {
            //Usereingabe
            int[] TempNetAdress = InputIPv4.GetNetAdressFromUser();
            List<Tuple<string, int>> TempSubNetNameAndHosts = new();
            List<Tuple<int[], int>> TempSubNetAdressNextPräfix = new();
            List<Tuple<int[], int[], int>> TempSubNetAdress = new();
            bool Loop = true;
            while (Loop)
            {
                TempSubNetNameAndHosts.Add(InputIPv4.GetSubnetDataFormUser());
                Console.Write("Möchten Sie ein weiteres Teilnetzwerk eingeben? [j/n]: ");
                if (!(Console.ReadLine() == "j"))
                {
                    MethodenIPv4.NetworksSort(TempSubNetNameAndHosts);
                    if (InputIPv4.UserDialog(TempSubNetNameAndHosts))
                        break;
                    else
                        TempSubNetNameAndHosts.Clear();
                } 
            }
            //Verarbeitung
            int[] TempNullNet = TempNetAdress;
            int count = 0;
            foreach (var Element in TempSubNetNameAndHosts)
            {
                TempSubNetAdressNextPräfix.Add(MethodenIPv4.NetworkJump(Element, TempNullNet));
                TempNullNet = new List<int>(TempSubNetAdressNextPräfix[count].Item1).ToArray();
                count++;
            }
            for (int i = 0; i < TempSubNetNameAndHosts.Count; i++)
            {
                TempSubNetAdress.Add(new Tuple<int[], int[], int>(TempNullNet, MethodenIPv4.GetBrodcast(TempSubNetAdressNextPräfix[i]), TempSubNetAdressNextPräfix[i].Item2));
            }
            NetAdress = TempNetAdress;
            SubNetNameAndHosts = TempSubNetNameAndHosts;
            SubNetAdress = TempSubNetAdress;
            
        }
    }
}
