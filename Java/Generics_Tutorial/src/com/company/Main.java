package com.company;


/*This example shows the use of Generics by illustrating the ability to define in a class
the type so that when we call a method for that class we are ensureing that we are only
sending the correct object type i.e. can not add a football player to a baseball team.. etc
 */
public class Main {

    public static void main(String[] args) {

        //Create some player objects
        BaseballPlayer baseballPlayer1 = new BaseballPlayer("Bialas");
        BaseballPlayer baseballPlayer2 = new BaseballPlayer("Arron");
        FootballPlayer footballPlayer1 = new FootballPlayer("Smith");
        SoccerPlayer soccerPlayer1 = new SoccerPlayer("Jones");
        SoccerPlayer soccerPlayer2 = new SoccerPlayer("Hammon");

        //Crate the team
        //Instantite the class with the class type of Footballplayer
        Team<FootballPlayer> theTeam = new Team<>("Super Stars");

        //Add these players to the team
        theTeam.addPlayer(footballPlayer1);
        //can not do this.  theTeam is instantiated from Team which has been typed as FootballTeam
        //no other player types, e.g. baseball/soccer allowed
//        theTeam.addPlayer(baseballPlayer1);
//        theTeam.addPlayer(soccerPlayer1);
//        theTeam.addPlayer(soccerPlayer2);

        //show size of the team
        System.out.println("There are " + theTeam.numPlayers() + " players on the team");

        //Create a new baseball team and add a baseball player
        Team<BaseballPlayer> baseballTeam1= new Team<>("Detroit Tigers");
        baseballTeam1.addPlayer(baseballPlayer1);
        //again this is not allowed no scocerplay class types allowed
//        baseballTeam.addPlayer(soccerPlayer2);

        Team<BaseballPlayer> baseballTeam2= new Team<>("New York Yankees");
        baseballTeam1.addPlayer(baseballPlayer2);

        baseballTeam1.matchResults(baseballTeam2, 12,2);

        System.out.println("Rankings " + baseballTeam1.getName() + ":" +baseballTeam1.ranking());
        System.out.println(baseballTeam1.compareTo(baseballTeam2));


    }
}
