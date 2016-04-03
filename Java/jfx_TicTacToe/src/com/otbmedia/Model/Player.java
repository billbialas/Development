package com.otbmedia.Model;

/**
 * Created by wbialas on 3/31/2016.
 */
public class Player {
    private String playerName;
    private int playerWins;
    private String playerMarker;

    public Player(String playerName, int playerWins, String playerMarker) {
        this.playerName = playerName;
        this.playerWins = playerWins;
        this.playerMarker = playerMarker;
    }

    public String getPlayerName() {
        return playerName;
    }

    public void setPlayerName(String playerName) {
        this.playerName = playerName;
    }

    public int getPlayerWins() {
        return playerWins;
    }

    public void setPlayerWins(int playerWins) {
        this.playerWins = playerWins;
    }

    public String getPlayerMarker() {
        return playerMarker;
    }

    public void setPlayerMarker(String playerMarker) {
        this.playerMarker = playerMarker;
    }
}
