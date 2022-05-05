**Magic: The Gathering Deck Builder

#### By **Nathan Conn, Nicholas Sullivan, Chris DePastene, Erin McCulley, Amber Wilwand**

#### This program was designed to build different tyeps of decks that could be stored in a local database and assigned to a user's profile. 


## Technologies Used

* C#
* .NET 5.0
* MVC
* Scryfall API



## Description

The program will make an API call to the Scryfall API and look up a card based on a part of or the whole name, a color identity, and/or the type of spell you are looking to add to your deck. The search results will yield all possible matches and display the mana cost, the set it was first published in, and the type of spell it is. The details page will display when the user clicks on a name and will be able to see what play formats it is allowed in and the prices for each type of card between the US and Europe.

## Setup/Installation Requirements

* Clone repository.
* In the command line, run "dotnet restore" in the MTGDeck directory.
* In the command line, run "dotnet build" in  the MTGDeck and directory.
* In the command line, run "dotnet run" while in the MTGDeck directory to run the program.
* You will need to create an appsettings.json file in order to use and store information into the database that is attached. Use the code below and replace the necessary fields with your information:

{
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Port=3306;database=DBNAMEHERE;uid=root;pwd=PASSWORDHERE;"
    }
}

* In the command line, run "dotnet ef migrations add Initial" (and label each one different with any update you make)
* In the command line, run "dotnet ef database update"


## Known Bugs

*Currently no known bugs.

## Contact Me

Let me know if you run into any issues or have questions, ideas, or concerns:
Co-authored-by: Nathan Conn <nconn34@gmail.com>
Co-authored-by: Nick Sullivan <nicholassull@gmail.com>
Co-authored-by: Amber Wilwand <amoothenielson@gmail.com>
Co-authored-by: Chris DePastene <cdepastene@gmail.com>
Co-authored-by: Erin McCulley <ejmcculley@gmail.com>	

## License

Copyright (c) 5/4/2022 Co-authored-by: Nathan Conn <nconn34@gmail.com>
Co-authored-by: Nick Sullivan <nicholassull@gmail.com>
Co-authored-by: Amber Wilwand <amoothenielson@gmail.com>
Co-authored-by: Chris DePastene <cdepastene@gmail.com>
Co-authored-by: Erin McCulley <ejmcculley@gmail.com>
