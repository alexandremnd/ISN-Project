# Projet ISN
 Jeu vidéo du projet en ISN en cours de développement par Danyo- and Duffy.

## Introduction

 L'objectif final est de produire un jeu vidéo sous Unity :smile: .

## Conventions de branches
 * On utilisera deux branches : master, develop
 * Lors du développement, on dérivera de develop
 * Quand on a terminé une version, on merge develop avec master, et on tague master

## Conventions de disposition
 * Ecrire une seule instruction par ligne
 * Ecrire une seule déclaration par ligne
 * Ajoutez une ligne blanche entre les définitions des méthodes (fonctions) et les définitions des propriétés.
 * Utilisez des parenthèses pour rendre apparente les clauses d'une expression
 > if ((val1 > val2) && (val1 > val3))

## Conventions de commentaires
 * Placez le commentaire sur une ligne séparée, pas à la fin d'une ligne de code
 * Commencez le commentaire par une lettre majuscule
 * Terminer le texte de commentaire par un point
 * Insérez un espace entre le délimiteur de commentaire et le texte du commentaire

## Conventions générales d'affectation de nom
 ### Choix de mots
  * Choisir des noms d'identificateurs lisibles.
  * Privilégier la lisibilité à la concision
  > Exemple : CanScrollHoriontally est mieux que ScrollableX
  * On évite d'utiliser des mots 
   déjà utiliser par le langage C#.
  
 ### Utilisation des abréviations
  * On utilise pas d'abbréviation ou contractions 
  > Exemple : GetWindow plutôt que GetWin

 ### Conventions de nom
| Object Name               | Notation   | Length | Plural | Prefix | Suffix | Abbreviation | Char Mask          | Underscores |
|:--------------------------|:-----------|-------:|:-------|:-------|:-------|:-------------|:-------------------|:------------|
| Class name                | PascalCase |    128 | No     | No     | Yes    | No           | [A-z][0-9]         | No          |
| Constructor name          | PascalCase |    128 | No     | No     | Yes    | No           | [A-z][0-9]         | No          |
| Method name               | PascalCase |    128 | Yes    | No     | No     | No           | [A-z][0-9]         | No          |
| Method arguments          | camelCase  |    128 | Yes    | No     | No     | Yes          | [A-z][0-9]         | No          |
| Local variables           | camelCase  |     50 | Yes    | No     | No     | Yes          | [A-z][0-9]         | No          |
| Constants name            | PascalCase |     50 | No     | No     | No     | No           | [A-z][0-9]         | No          |
| Field name                | camelCase  |     50 | Yes    | No     | No     | Yes          | [A-z][0-9]         | Yes         |
| Properties name           | PascalCase |     50 | Yes    | No     | No     | Yes          | [A-z][0-9]         | No          |
| Delegate name             | PascalCase |    128 | No     | No     | Yes    | Yes          | [A-z]              | No          |
| Enum type name            | PascalCase |    128 | Yes    | No     | No     | No           | [A-z]              | No          |

## Installation

 Ceci est actuellement le projet unity pur, vous devez savoir comment utiliser le moteur de jeu Unity pour être capable de compiler le jeu. Si vous souhaitez uniquement jouer au jeu, veuillez attendre la sortie finale.
