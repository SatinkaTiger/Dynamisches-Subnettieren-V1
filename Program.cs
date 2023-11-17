namespace Dynamisches_Subnettieren_V1
{
    using Dynamisches_Subnettieren_V1.IPv4;
    using Dynamisches_Subnettieren_V1.UserInterface;

    internal class Program
    {
        static void Main(string[] args)
        {
            //User Eingaben
            switch (UserInterfaceEingabe.GetIpModeFromUser())
            {
                case 1:
                    DynamischIPv4.Start();
                    UserInterfaceAusgabe.StartDynamischIPv4(DynamischIPv4.SubNetAdress,DynamischIPv4.SubNetNameAndHosts);
                    break;
                case 2:
                    Console.WriteLine("IPv6 nicht implementiert qwq");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Es ist ein unerwarteter Fehler aufgetreten");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
