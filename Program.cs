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

            Console.WriteLine("Welche Sprache ist die 1 Sprache?");
            string vociSprache1 = Console.ReadLine();

            Console.WriteLine("Welche Sprache ist die 2 Sprache?");
            string vociSprache2 = Console.ReadLine();

            File.AppendAllText(dateiName, vociSprache1 + ";" + vociSprache2 + Environment.NewLine);

            string weiter = "ja";

            while (weiter == "ja")
            {
                Console.WriteLine("Wort auf " + vociSprache1);
                string wort1 = Console.ReadLine();

                Console.WriteLine("Wort auf " + vociSprache2);
                string wort2 = Console.ReadLine();

                File.AppendAllText(dateiName, wort1 + ";" + wort2 + Environment.NewLine);

                Console.WriteLine("Noch ein Wort hinzufügen? (ja oder nein)");
                weiter = Console.ReadLine();
            }
        }
    }
    else if (newVoci == 3)
    {

        string[] vociDateien = Directory.GetFiles(".", "*.txt");

        Console.WriteLine("Vorhandene Voci-Sets:");

        foreach (string datei in vociDateien)
        {
            Console.WriteLine(Path.GetFileNameWithoutExtension(datei));
        }

        Console.WriteLine("Zu welchem Voci wollen Sie hinzufügen?");
        string vociName = Console.ReadLine();

        string dateiName = vociName + ".txt";

        if (File.Exists(dateiName))
        {
            string weiter = "ja";

            string[] zeilen = File.ReadAllLines(dateiName);
            string[] sprachen = zeilen[0].Split(';');
            string sprache1 = sprachen[0];
            string sprache2 = sprachen[1];

            while (weiter == "ja")
            {
                Console.WriteLine("Wort auf " + sprache1);
                string wort1 = Console.ReadLine();

                Console.WriteLine("Wort auf " + sprache2);
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

        string[] vociDateien = Directory.GetFiles(".", "*.txt");

        Console.WriteLine("Vorhandene Voci-Sets:");

        foreach (string datei in vociDateien)
        {
            Console.WriteLine(Path.GetFileNameWithoutExtension(datei));
        }

        Console.WriteLine("Welches Voci wollen Sie lernen?");
        string vociName = Console.ReadLine();

        int richtig = 0;
        int insgesamt = 0;

        string dateiName = vociName + ".txt";

        if (File.Exists(dateiName))
        {
            string[] zeilen = File.ReadAllLines(dateiName);

            string[] sprachen = zeilen[0].Split(';');
            string sprache1 = sprachen[0];
            string sprache2 = sprachen[1];

            Console.WriteLine("Welche Richtung willst du lernen?");
            Console.WriteLine("von " + sprache1 + " nach " + sprache2 + " (1)");
            Console.WriteLine("oder");
            Console.WriteLine("von " + sprache2 + " nach " + sprache1 + " (2)");
            int richtung = 0;

            while (richtung != 1 && richtung != 2)
            {
                richtung = Convert.ToInt32(Console.ReadLine());

                if (richtung != 1 && richtung != 2)
                {
                    Console.WriteLine("falsche eingabe bitte geben sie 1 oder 2 ein");
                }
            }

            zeilen = zeilen.Skip(1).ToArray();

            Random random = new Random();

            zeilen = zeilen.OrderBy(x => random.Next()).ToArray();

            if (richtung == 1)
            {
                foreach (string zeile in zeilen)
                {
                    string[] woerter = zeile.Split(';');

                    string wort1 = woerter[0];
                    string wort2 = woerter[1];

                    Console.WriteLine(sprache1 + ": " + wort1 + " was ist das wort auf " + sprache2);
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
            }
            else
            {
                foreach (string zeile in zeilen)
                {
                    string[] woerter = zeile.Split(';');

                    string wort1 = woerter[0];
                    string wort2 = woerter[1];

                    Console.WriteLine(sprache2 + ": " + wort2 + " was ist das wort auf " + sprache1);
                    string eingabeWort = Console.ReadLine();

                    if (eingabeWort == wort1)
                    {
                        Console.WriteLine("Richtig: " + wort2 + " ist " + wort1);
                        richtig = richtig + 1;
                        insgesamt = insgesamt + 1;

                    }
                    else
                    {
                        Console.WriteLine("falsch: " + wort2 + " ist " + wort1);
                        insgesamt = insgesamt + 1;
                    }
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
