package com.company;

import java.util.ArrayList;

/**
 * Created by wbialas on 3/14/2016.
 *
 * This class will do all the hard work
 * Methods:
 *  Add a player to the team (check if he exists)
 *  Return the number of players on the team
 *  Match results to determin if instantiated class won, lost, tied opponent
 *  Return ranking
 *
 */

// Key point of this example is the generic type <T> here, in the Arraylist, and the addPlayer method
// this will allow this class to be generic to add anytype of player
// when we instantiate the class the <T> will be replaced auto by java with the acutal class we are using
// using the extends Player in the declaration only allows any class/subclass that extends from Player (class)
// example of a bounded class, in this case it the upper
// We are going to also implemnt the Compariable interface to be able to compare teams the Comparable<Team<T>
// ensures we are only comparing similar teams, e.g Baseball to Baseball
public class Team<T extends Player> implements Comparable<Team<T>> { //This is the class declaration

    //Team name
    private String name;
    int played=0;
    int won=0;
    int lost=0;
    int tied=0;

    //Create an Array to hold all the players that will be on this team
    private ArrayList<T> members = new ArrayList<>();

    public Team(String name) {
        this.name = name;
    }

    public String getName() {
        return name;
    }

    //Method to add a player to the team
    public boolean addPlayer(T player){

        //Check and see if the player is already on the team
        if (this.members.contains(player)){
            System.out.println(player.getName() + " is already on the team");
            return false;
        }else {
            this.members.add(player);
            System.out.println(player.getName() + " was picked for the team " + this.name);
            return true;
        }

    }

    //method to return the number of players on the team
    public int numPlayers(){
        return this.members.size();
    }

    //method to determine if calling team, meaning the instantiated object, won, lost, tied vs the opponent team passed
    // the <t> lets us define the type to ensure that we are only sending the correc type to this method
    public void matchResults(Team<T> opponent, int ourScore, int theirScore){
        if (ourScore>theirScore){
            won++;
            System.out.println(this.name + " won over " + opponent.getName());
        }else if (ourScore == theirScore){
            tied++;
            System.out.println(this.name + " tied over " + opponent.getName());
        } else {
            lost++;
            System.out.println(this.name + " lost over " + opponent.getName());
        }
        played++;
        //This section of code will also keep score for the opposing team
//        if (opponent != null){
//            opponent.matchResults(null, theirScore, ourScore);
//        }
    }

    public int ranking(){
        return (won*2)+tied;
    }

    //Override method for the Compariable interface
    @Override
    public int compareTo(Team<T> team) {
        if (this.ranking()> team.ranking()){
            return -1;
        }else if (this.ranking() < team.ranking()) {
            return 1;
        } else {
            return 0;
        }
    }


}
