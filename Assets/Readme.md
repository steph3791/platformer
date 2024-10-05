# CubeDash
Einfacher 2D Platformer

## Controls
- A: Nach Links bewegen
- D: Nach Rechts bewegen
- Space: Springen
- ESC: Pausieren

Der Spieler kann zwei mal springen, der zweite Sprung verliert jedoch an Kraft.

An bestimmten Stellen kann der Spieler von Wand zu Wand springen.

## Umsetzung der Anforderungen
- **Das Spiel soll sinnvollen Gebrauch vom Input System machen:** Das Input System wurde für alle Steuerungsaspekte verwendet (siehe Controls)
- **Das Projekt muss von verschiedenen Scenes sinnvoll gebrauch machen und zwischen Scenes wechseln:**
- **Die erste der Scenes muss ein Main Menu mit UI sein:** Main Menu mit Start, Optionen und Exit
- **Das Projekt soll sinnvollen Gebrauch von C#-Scripts machen:** Sehr diverser Gebrauch von Scripts über UI Logik, Aktivierung und Deaktivierung von Components, etwas komplexere Physik Logik mit Anpassung der Gravity, Doppelsprüngen und Wall Jumps usw.
- **Das Spiel soll pausierbar sein:** Pausierbar durch drücken von escape. Vom Menu kann man auf die Optionen Zugreifen, das Spiel verlassen oder weiterfahren
- **Das Spiel soll sinnvoller Gebrauch vom Physics System machen, inklusive Rigidbodies und Colliders und Triggers:** Diverser Gebrauch von Colliders, Rigidbodies und Triggers. Für Checkpoints werden beispielsweise Triggers verwendet und für Obstacles Collider.
- **Das Spiel muss sinnvollen Gebrauch vom Audio System machen:** Verwendung von diversen Effekts. Ein Hintergrund Track im Loop, sowie Jump, Damage und Win Sound Effekte
- **Verwendet ein oder mehrere Assets aus dem Asset Store:** Die Tiles für die Umgebung wurden vom Asset Store bezogen.

## Zusätzliche Features
- 2D Kontext statt 3D
- Verwendung von Tilemaps 
- Daten Persistenz über Szenen (Optionen Manager -> GameFlowManager, PlayerOptionsManager)
- Soft Body Physik: Dieses Feature konnte ich nicht final implementieren da es unzählige Probleme gab die Investierte Zeit sehr schnell den vorgeschriebenen Zeitrahmen aufrass, ohne dass das Spiel an sich irgendwelche Fortschritte machte. Grundsätzlich habe ich hier mithilfe des Sprite Editors Bones und Springs verwendet mit dem Ziel, einen "Jelly-like" Charakter zu generieren statt den aktuell implementierten einfachen Würfel.
 