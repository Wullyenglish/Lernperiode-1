# Lern-Periode 1

## fertiges Projekt

Mein Programm ist ein Voci-Trainer. Man kann Voci-Sets erstellen, bearbeiten und löschen. Beim Lernen werden die Wörter abgefragt und am Schluss sieht man die Trefferquote.


<img width="400" height="318" alt="Bildschirmaufnahme 2026-09-18 161541" src="https://github.com/user-attachments/assets/9ce67d80-ec90-4f8a-8bd8-77d8389eb8e7" />


## Grob-Planung
C# programm zum Voci lernen das verschiedene voci kann speichern und wieder geben
## 28.8.2026

Heute habe ich mit meinem C#-Programm zum Voci-Lernen angefangen. Ich habe ein Hauptmenü erstellt, in dem man ein neues Voci erstellen, ein bestehendes Voci lernen oder neue Wörter hinzufügen kann. Die Vokabeln werden in Textdateien gespeichert und können später wieder ausgelesen werden. Beim Lernen werden die Wörter zufällig gemischt und danach abgefragt. Zusätzlich habe ich angefangen zu zählen, wie viele Antworten richtig und wie viele insgesamt beantwortet wurden.

## 6.9.2026

- [x] Beim Erstellen eines Voci-Sets die beiden Sprachen speichern
- [x] Beim Lernen oder Bearbeiten eine Liste mit allen vorhandenen Voci-Sets anzeigen
- [x] Beim Auswählen eines Voci-Sets die gespeicherten Sprachen anzeigen und für die Abfrage verwenden
- [x] Die Sprache switchen welch das angezeigt wird und welche das man muss Reinschreiben
      
Heute habe ich die Sprachen der Voci-Sets gespeichert, vorhandene Sets anzeigen lassen und die gespeicherten Sprachen beim Lernen und Bearbeiten verwendet. Ausserdem kann man jetzt auswählen, in welche Richtung man die Voci lernen möchte.

## 11.9.2026

- [x] Das Konsolenprojekt für Windows Forms vorbereiten
- [x] In Program.cs ein Hauptfenster für den Voci-Trainer erstellen
- [x] Buttons für Voci erstellen, lernen, hinzufügen und beenden einbauen
- [x] Console.WriteLine und Console.ReadLine durch Fenster, Textfelder und Labels ersetzen

Ich habe mein ursprüngliches Konsolenprogramm in eine einfache grafische Version umgebaut. Dabei habe ich die eigentliche Logik mit den Voci-Dateien, den if-Abfragen, Schleifen und dem Lernen weitgehend gleich gelassen.

Console.WriteLine() habe ich durch MessageBox.Show() ersetzt, damit Ausgaben in kleinen Fenstern erscheinen. Console.ReadLine() habe ich durch Interaction.InputBox() ersetzt, damit Eingaben über ein Textfeld gemacht werden können. Für echte Auswahlen, zum Beispiel das Hauptmenü, die Lernrichtung oder die Auswahl eines Voci-Sets, habe ich Buttons verwendet.

Zusätzlich habe ich noch eingebaut, dass falsche oder leere Eingaben abgefangen werden, Wörter bearbeitet oder gelöscht werden können, falsch beantwortete Wörter am Schluss nochmals kommen und nach dem Lernen die Trefferquote in Prozent angezeigt wird.

## Hausaufgaben vom 11.9.2026

- [x] Beim Lernen eine zufällige Reihenfolge der Voci auswählen können
- [x] Doppelte Voci-Einträge verhindern

Ich habe zwei neue Funktionen eingebaut. Man kann jetzt auswählen, ob die Voci normal oder zufällig kommen. Ausserdem werden doppelte Voci nicht mehr gespeichert.

    
## 18.9.2026

- [x] Ein ganzes Voci-Set umbenennen können
- [x] Ein ganzes Voci-Set löschen können
- [x] Die letzte Trefferquote eines Voci-Sets speichern

Heute habe ich drei neue Funktionen eingebaut. Man kann jetzt ganze Voci-Sets umbenennen und löschen. Ausserdem wird die letzte Trefferquote gespeichert und später wieder angezeigt. Am schwierigsten war für mich, dass immer die richtige Datei ausgewählt und geändert wird. Auch das Speichern der Trefferquote war etwas schwierig, weil sie in der Datei bleiben musste.

##Reflexion


