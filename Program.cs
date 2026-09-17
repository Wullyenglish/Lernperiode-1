using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Voci_Trainer
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            int newVoci = 0;

            while (true)
            {
                newVoci = Auswahl(
                    "Was möchtest du machen?",
                    "Neues Voci erstellen",
                    "Voci lernen",
                    "Wörter hinzufügen",
                    "Wörter bearbeiten / löschen",
                    "Programm beenden"
                );

                // Neues Voci erstellen
                if (newVoci == 1)
                {
                    string vociName = Interaction.InputBox(
                        "Geben Sie den Namen des Voci-Themas ein:",
                        "Voci Trainer"
                    );

                    if (string.IsNullOrWhiteSpace(vociName))
                    {
                        MessageBox.Show("Bitte einen Namen eingeben.");
                        continue;
                    }

                    if (vociName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                    {
                        MessageBox.Show("Dieser Name ist ungültig.");
                        continue;
                    }

                    string dateiName = vociName + ".txt";

                    if (File.Exists(dateiName))
                    {
                        MessageBox.Show("Ein Voci mit diesem Namen existiert bereits.");
                    }
                    else
                    {
                        File.Create(dateiName).Close();

                        MessageBox.Show("Das Voci wurde erstellt.");

                        string vociSprache1 = Interaction.InputBox(
                            "Welche Sprache ist die 1. Sprache?",
                            "Voci Trainer"
                        );

                        string vociSprache2 = Interaction.InputBox(
                            "Welche Sprache ist die 2. Sprache?",
                            "Voci Trainer"
                        );

                        if (vociSprache1 == "" || vociSprache2 == "")
                        {
                            MessageBox.Show("Bitte beide Sprachen eingeben.");
                            File.Delete(dateiName);
                            continue;
                        }

                        File.AppendAllText(
                            dateiName,
                            vociSprache1 + ";" + vociSprache2 + Environment.NewLine
                        );

                        string weiter = "ja";

                        while (weiter == "ja")
                        {
                            string wort1 = Interaction.InputBox(
                                "Wort auf " + vociSprache1,
                                "Voci Trainer"
                            );

                            string wort2 = Interaction.InputBox(
                                "Wort auf " + vociSprache2,
                                "Voci Trainer"
                            );

                            if (wort1 == "" || wort2 == "")
                            {
                                MessageBox.Show("Bitte beide Wörter eingeben.");
                            }
                            else
                            {
                                string neuerEintrag = wort1 + ";" + wort2;

                                string[] vorhandeneWoerter = File.ReadAllLines(dateiName);

                                if (vorhandeneWoerter.Skip(1).Contains(neuerEintrag))
                                {
                                    MessageBox.Show("Dieses Voci existiert bereits.");
                                }
                                else
                                {
                                    File.AppendAllText(
                                        dateiName,
                                        neuerEintrag + Environment.NewLine
                                    );
                                }
                            }

                            weiter = JaNein("Noch ein Wort hinzufügen?");
                        }
                    }
                }

                // Wörter hinzufügen
                else if (newVoci == 3)
                {
                    string[] vociDateien = Directory.GetFiles(".", "*.txt");

                    if (vociDateien.Length == 0)
                    {
                        MessageBox.Show("Es gibt noch keine Voci-Sets.");
                        continue;
                    }

                    string[] vociNamen = vociDateien
                        .Select(Path.GetFileNameWithoutExtension)
                        .ToArray();

                    int vociAuswahl = Auswahl(
                        "Zu welchem Voci wollen Sie hinzufügen?",
                        vociNamen
                    );

                    if (vociAuswahl == 0)
                    {
                        continue;
                    }

                    string vociName = vociNamen[vociAuswahl - 1];

                    string dateiName = vociName + ".txt";

                    if (File.Exists(dateiName))
                    {
                        string weiter = "ja";

                        string[] zeilen = File.ReadAllLines(dateiName);

                        if (zeilen.Length == 0 || !zeilen[0].Contains(";"))
                        {
                            MessageBox.Show("Dieses Voci-Set ist fehlerhaft.");
                            continue;
                        }

                        string[] sprachen = zeilen[0].Split(';');

                        string sprache1 = sprachen[0];
                        string sprache2 = sprachen[1];

                        while (weiter == "ja")
                        {
                            string wort1 = Interaction.InputBox(
                                "Wort auf " + sprache1,
                                "Voci Trainer"
                            );

                            string wort2 = Interaction.InputBox(
                                "Wort auf " + sprache2,
                                "Voci Trainer"
                            );

                            if (wort1 == "" || wort2 == "")
                            {
                                MessageBox.Show("Bitte beide Wörter eingeben.");
                            }
                            else
                            {
                                string neuerEintrag = wort1 + ";" + wort2;

                                string[] vorhandeneWoerter = File.ReadAllLines(dateiName);

                                if (vorhandeneWoerter.Skip(1).Contains(neuerEintrag))
                                {
                                    MessageBox.Show("Dieses Voci existiert bereits.");
                                }
                                else
                                {
                                    File.AppendAllText(
                                        dateiName,
                                        neuerEintrag + Environment.NewLine
                                    );
                                }
                            }

                            weiter = JaNein("Noch ein Wort hinzufügen?");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ungültiger Voci-Name.");
                    }
                }

                // Voci lernen
                else if (newVoci == 2)
                {
                    string[] vociDateien = Directory.GetFiles(".", "*.txt");

                    if (vociDateien.Length == 0)
                    {
                        MessageBox.Show("Es gibt noch keine Voci-Sets.");
                        continue;
                    }

                    string[] vociNamen = vociDateien
                        .Select(Path.GetFileNameWithoutExtension)
                        .ToArray();

                    int vociAuswahl = Auswahl(
                        "Welches Voci wollen Sie lernen?",
                        vociNamen
                    );

                    if (vociAuswahl == 0)
                    {
                        continue;
                    }

                    string vociName = vociNamen[vociAuswahl - 1];

                    int richtig = 0;
                    int insgesamt = 0;

                    List<string> falscheWoerter = new List<string>();

                    string dateiName = vociName + ".txt";

                    if (File.Exists(dateiName))
                    {
                        string[] zeilen = File.ReadAllLines(dateiName);

                        if (zeilen.Length <= 1 || !zeilen[0].Contains(";"))
                        {
                            MessageBox.Show("Dieses Voci enthält keine Wörter.");
                            continue;
                        }

                        string[] sprachen = zeilen[0].Split(';');

                        string sprache1 = sprachen[0];
                        string sprache2 = sprachen[1];

                        int richtung = Auswahl(
                            "Welche Richtung willst du lernen?",
                            sprache1 + " → " + sprache2,
                            sprache2 + " → " + sprache1
                        );

                        if (richtung == 0)
                        {
                            continue;
                        }

                        zeilen = zeilen.Skip(1).ToArray();

                        int reihenfolge = Auswahl(
                            "Welche Reihenfolge willst du lernen?",
                            "Normale Reihenfolge",
                            "Zufällige Reihenfolge"
                        );

                        if (reihenfolge == 0)
                        {
                            continue;
                        }

                        if (reihenfolge == 2)
                        {
                            Random random = new Random();

                            zeilen = zeilen.OrderBy(x => random.Next()).ToArray();
                        }

                        if (richtung == 1)
                        {
                            foreach (string zeile in zeilen)
                            {
                                string[] woerter = zeile.Split(';');

                                string wort1 = woerter[0];
                                string wort2 = woerter[1];

                                string eingabeWort = Interaction.InputBox(
                                    sprache1 + ": " + wort1 +
                                    "\nWas ist das Wort auf " + sprache2 + "?",
                                    "Voci Trainer"
                                );

                                if (eingabeWort == wort2)
                                {
                                    MessageBox.Show(
                                        "Richtig: " + wort1 + " ist " + wort2
                                    );

                                    richtig = richtig + 1;
                                }
                                else
                                {
                                    MessageBox.Show(
                                        "Falsch: " + wort1 + " ist " + wort2
                                    );

                                    falscheWoerter.Add(zeile);
                                }

                                insgesamt = insgesamt + 1;
                            }
                        }
                        else
                        {
                            foreach (string zeile in zeilen)
                            {
                                string[] woerter = zeile.Split(';');

                                string wort1 = woerter[0];
                                string wort2 = woerter[1];

                                string eingabeWort = Interaction.InputBox(
                                    sprache2 + ": " + wort2 +
                                    "\nWas ist das Wort auf " + sprache1 + "?",
                                    "Voci Trainer"
                                );

                                if (eingabeWort == wort1)
                                {
                                    MessageBox.Show(
                                        "Richtig: " + wort2 + " ist " + wort1
                                    );

                                    richtig = richtig + 1;
                                }
                                else
                                {
                                    MessageBox.Show(
                                        "Falsch: " + wort2 + " ist " + wort1
                                    );

                                    falscheWoerter.Add(zeile);
                                }

                                insgesamt = insgesamt + 1;
                            }
                        }

                        // Falsche Wörter nochmals
                        if (falscheWoerter.Count > 0)
                        {
                            MessageBox.Show(
                                "Die falschen Wörter werden nochmals abgefragt."
                            );

                            foreach (string zeile in falscheWoerter)
                            {
                                string[] woerter = zeile.Split(';');

                                string wort1 = woerter[0];
                                string wort2 = woerter[1];

                                if (richtung == 1)
                                {
                                    string eingabeWort = Interaction.InputBox(
                                        sprache1 + ": " + wort1 +
                                        "\nWas ist das Wort auf " + sprache2 + "?",
                                        "Voci Trainer"
                                    );

                                    if (eingabeWort == wort2)
                                    {
                                        MessageBox.Show("Richtig!");
                                    }
                                    else
                                    {
                                        MessageBox.Show(
                                            "Falsch. Richtig ist: " + wort2
                                        );
                                    }
                                }
                                else
                                {
                                    string eingabeWort = Interaction.InputBox(
                                        sprache2 + ": " + wort2 +
                                        "\nWas ist das Wort auf " + sprache1 + "?",
                                        "Voci Trainer"
                                    );

                                    if (eingabeWort == wort1)
                                    {
                                        MessageBox.Show("Richtig!");
                                    }
                                    else
                                    {
                                        MessageBox.Show(
                                            "Falsch. Richtig ist: " + wort1
                                        );
                                    }
                                }
                            }
                        }

                        // Trefferquote
                        double prozent = (double)richtig / insgesamt * 100;

                        MessageBox.Show(
                            "Du hattest " + richtig +
                            " von " + insgesamt +
                            " Wörtern richtig.\n" +
                            "Trefferquote: " + Math.Round(prozent) + "%"
                        );
                    }
                    else
                    {
                        MessageBox.Show("Dieses Voci existiert nicht.");
                    }
                }

                // Wörter bearbeiten oder löschen
                else if (newVoci == 4)
                {
                    string[] vociDateien = Directory.GetFiles(".", "*.txt");

                    if (vociDateien.Length == 0)
                    {
                        MessageBox.Show("Es gibt noch keine Voci-Sets.");
                        continue;
                    }

                    string[] vociNamen = vociDateien
                        .Select(Path.GetFileNameWithoutExtension)
                        .ToArray();

                    int vociAuswahl = Auswahl(
                        "Welches Voci wollen Sie bearbeiten?",
                        vociNamen
                    );

                    if (vociAuswahl == 0)
                    {
                        continue;
                    }

                    string vociName = vociNamen[vociAuswahl - 1];
                    string dateiName = vociName + ".txt";

                    string[] zeilen = File.ReadAllLines(dateiName);

                    if (zeilen.Length <= 1)
                    {
                        MessageBox.Show("Dieses Voci enthält keine Wörter.");
                        continue;
                    }

                    string[] sprachen = zeilen[0].Split(';');

                    string[] woerterAnzeige = zeilen
                        .Skip(1)
                        .Select(x => x.Replace(";", " = "))
                        .ToArray();

                    int wortAuswahl = Auswahl(
                        "Welches Wort wollen Sie bearbeiten?",
                        woerterAnzeige
                    );

                    if (wortAuswahl == 0)
                    {
                        continue;
                    }

                    int aktion = Auswahl(
                        "Was möchten Sie machen?",
                        "Bearbeiten",
                        "Löschen"
                    );

                    // Bearbeiten
                    if (aktion == 1)
                    {
                        string[] wort = zeilen[wortAuswahl].Split(';');

                        string neuesWort1 = Interaction.InputBox(
                            "Wort auf " + sprachen[0],
                            "Voci Trainer",
                            wort[0]
                        );

                        string neuesWort2 = Interaction.InputBox(
                            "Wort auf " + sprachen[1],
                            "Voci Trainer",
                            wort[1]
                        );

                        if (neuesWort1 != "" && neuesWort2 != "")
                        {
                            zeilen[wortAuswahl] =
                                neuesWort1 + ";" + neuesWort2;

                            File.WriteAllLines(dateiName, zeilen);

                            MessageBox.Show("Das Wort wurde geändert.");
                        }
                    }

                    // Löschen
                    else if (aktion == 2)
                    {
                        zeilen = zeilen
                            .Where((x, i) => i != wortAuswahl)
                            .ToArray();

                        File.WriteAllLines(dateiName, zeilen);

                        MessageBox.Show("Das Wort wurde gelöscht.");
                    }
                }

                // Programm beenden
                else if (newVoci == 5 || newVoci == 0)
                {
                    break;
                }
            }
        }


        static string JaNein(string frage)
        {
            if (MessageBox.Show(
                frage,
                "Voci Trainer",
                MessageBoxButtons.YesNo
            ) == DialogResult.Yes)
            {
                return "ja";
            }

            return "nein";
        }


        static int Auswahl(string frage, params string[] optionen)
        {
            int auswahl = 0;

            Form fenster = new Form();

            fenster.Text = "Voci Trainer";
            fenster.Size = new Size(400, 120 + optionen.Length * 50);
            fenster.StartPosition = FormStartPosition.CenterScreen;
            fenster.AutoScroll = true;

            Label text = new Label();

            text.Text = frage;
            text.AutoSize = true;
            text.Location = new Point(30, 20);

            fenster.Controls.Add(text);

            for (int i = 0; i < optionen.Length; i++)
            {
                Button button = new Button();

                button.Text = optionen[i];
                button.Size = new Size(300, 40);
                button.Location = new Point(40, 55 + i * 45);

                int nummer = i + 1;

                button.Click += (sender, e) =>
                {
                    auswahl = nummer;
                    fenster.Close();
                };

                fenster.Controls.Add(button);
            }

            fenster.ShowDialog();

            return auswahl;
        }
    }
}
