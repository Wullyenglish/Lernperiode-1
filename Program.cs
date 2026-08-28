// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.IO;
using System.Linq;

int newVoci = 0;

while (true)
{

    Console.WriteLine("1 = Neues Voci erstellen");
    Console.WriteLine("2 = Voci lernen");
    Console.WriteLine("3 = Wörter hinzufügen");
    Console.WriteLine("4 = Programm beenden");
    newVoci = Convert.ToInt32(Console.ReadLine());

    if (newVoci == 1)
    {
        Console.WriteLine("Geben sie den Namen der Voci Thema ein:");
        string vociName = Console.ReadLine();

        string dateiName = vociName + ".txt";

        if (File.Exists(dateiName))
        {
            Console.WriteLine("Ein Voci mit diesem Namen existiert bereits.");
        }
        else
        {
            File.Create(dateiName).Close();
            Console.WriteLine("Das Voci wurde erstellt.");

            string weiter = "ja";

            while (weiter == "ja")
            {
                Console.WriteLine("Wort in Sprache 1:");
                string wort1 = Console.ReadLine();

                Console.WriteLine("Wort in Sprache 2:");
                string wort2 = Console.ReadLine();

                File.AppendAllText(dateiName, wort1 + ";" + wort2 + Environment.NewLine);

                Console.WriteLine("Noch ein Wort hinzufügen? (ja oder nein)");
                weiter = Console.ReadLine();
            }
        }
    }
    else if (newVoci == 3)
    {
                Console.WriteLine("Zu welchem Voci wollen Sie hinzufügen?");
                string vociName = Console.ReadLine();

                string dateiName = vociName + ".txt";

                if (File.Exists(dateiName))
                {
                    string weiter = "ja";

                    while (weiter == "ja")
                    {
                        Console.WriteLine("Wort in Sprache 1:");
                        string wort1 = Console.ReadLine();

                        Console.WriteLine("Wort in Sprache 2:");
                        string wort2 = Console.ReadLine();

                        File.AppendAllText(dateiName, wort1 + ";" + wort2 + Environment.NewLine);

                        Console.WriteLine("Noch ein Wort hinzufügen? (ja oder nein)");
                        weiter = Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Ungültiger Voci Namen bitte gib eins ein das existiert");
                }
    }
    else if (newVoci == 2)
    {
        Console.WriteLine("Welches Voci wollen Sie lernen?");
        string vociName = Console.ReadLine();

        int richtig = 0;
        int insgesamt = 0;

        string dateiName = vociName + ".txt";

        if (File.Exists(dateiName))
        {
            string[] zeilen = File.ReadAllLines(dateiName);

            Random random = new Random();

            zeilen = zeilen.OrderBy(x => random.Next()).ToArray();

            foreach (string zeile in zeilen)
            {
                string[] woerter = zeile.Split(';');

                string wort1 = woerter[0];
                string wort2 = woerter[1];

                Console.WriteLine("Sprache 1: " + wort1 + " was ist das wort in der Sprache 2?");
                string eingabeWort = Console.ReadLine();

                if (eingabeWort == wort2)
                {
                    Console.WriteLine("Richtig: " + wort1 + " ist " + wort2);
                    richtig = richtig + 1;
                    insgesamt = insgesamt + 1;

                }
                else
                {
                    Console.WriteLine("falsch: " + wort1 + " ist " + wort2);
                    insgesamt = insgesamt + 1;
                }
            }
            Console.WriteLine("Du hattest " + richtig + " von " + insgesamt + " Wörtern richtig.");
        }
        else
        {
            Console.WriteLine("Dieses Voci existiert nicht.");
        }
    }
    else if (newVoci == 4)
    {
        break;
    }
    else
    {
        Console.WriteLine("Ungültige Eingabe. Bitte geben Sie 1, 2, 3 oder 4 ein.");
    }
}