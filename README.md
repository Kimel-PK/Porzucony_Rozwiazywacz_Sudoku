# Porzucony Rozwiązywacz Sudoku

Porzucony projekt algorytmu do rozwiązywania sudoku z 2017 roku.

Projekt został stworzony na wersji Unity 5.6.1f1.

## Zawartość repozytorium

Plik `Demo.unity` zawiera scenę z przygotowaną siatką i przyciskami do obsługi skryptu. Aby poruszać się po planszy używamy klawiszy strzałek. Cyfr nie można wpisywać z użyciem klawiatury numerycznej. Klawisz `0` maże cyfrę w aktualnie wybranej kratce.

Plik `Sudoku.cs` zawiera skrypt rozwiązujący sudoku bez wykorzystania dedukcji. Analiza planszy opiera się na odrzucaniu cyfr, które nie mogą znaleźć się w danej kratce. Z tego powodu w obecnej postaci program potrafi rozwiązać tylko proste zagadki.

## Demo

https://user-images.githubusercontent.com/57668948/143685955-775da0e2-e309-41b8-8faa-567a1a6f1163.mp4
