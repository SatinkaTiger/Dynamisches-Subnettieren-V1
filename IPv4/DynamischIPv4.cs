namespace Dynamisches_Subnettieren_V1.IPv4
{
    using Dynamisches_Subnettieren_V1.UserInterface;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class DynamischIPv4
    {
        public static int[] NetAdress = new int[4];
        public static int Präfix;
        public static List<Tuple<string, int>> SubNetNameAndHosts = new();
        public static List<Tuple<int[], int[], int>> SubNetAdress = new();
        public static void Start()
        {
            //Usereingabe
            int[] TempNetAdress = InputIPv4.GetNetAdressFromUser();
            int TempPräfix = InputIPv4.GetPräfixFormUser();
            List<Tuple<string, int>> TempSubNetNameAndHosts = new();
            List<Tuple<int[], int>> TempSubNetAdressNextPräfix = new();
            List<Tuple<int[], int[], int>> TempSubNetAdress = new();
            bool Loop = true;
            while (Loop)
            {
                TempSubNetNameAndHosts.Add(InputIPv4.GetSubnetDataFormUser(TempPräfix));
                Console.Write("Möchten Sie ein weiteres Teilnetzwerk eingeben? [j/n]: ");
                if (!(Console.ReadLine() == "j"))
                {
                    if (InputIPv4.UserDialog(TempSubNetNameAndHosts))
                        break;
                    else
                        TempSubNetNameAndHosts.Clear();
                } 
            }
            //Verarbeitung
            TempSubNetNameAndHosts = MethodenIPv4.NetworksSort(TempSubNetNameAndHosts);
            foreach (var Element in TempSubNetNameAndHosts)
                TempSubNetAdressNextPräfix.Add(MethodenIPv4.NetworkJump(Element, TempNetAdress));
            int[] NullNet = TempNetAdress;
            int count = 0;
            foreach (var Element in TempSubNetAdressNextPräfix)
            {
                int[] TempNullNet = new List<int>( Element.Item1).ToArray();
                TempSubNetAdress.Add(new Tuple<int[], int[], int>(NullNet, MethodenIPv4.GetBrodcast(Element), Element.Item2));
                NullNet = TempNullNet;
                count++;
            }
            NetAdress = TempNetAdress;
            Präfix = TempPräfix;
            SubNetNameAndHosts = TempSubNetNameAndHosts;
            SubNetAdress = TempSubNetAdress;
        }
    }
}
