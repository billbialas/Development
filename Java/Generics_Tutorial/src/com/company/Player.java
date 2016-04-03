package com.company;

/**
 * Created by wbialas on 3/14/2016.
 */
public abstract class Player {
    //Name of the player
    private String name;

    //Constructor to init the player name
    public Player(String name) {
        this.name = name;
    }

    //Getter to fetch player name
    public String getName() {
        return name;
    }
}
