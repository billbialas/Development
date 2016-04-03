package com.company;

public class Main {

    public static void main(String[] args) {
        int score=50;
        String playername="Bialas";

        displayHighScorePositon(playername,score);
    }
    public static void displayHighScorePositon(String playername, int score){
        System.out.println(playername + " managed to get into position " + calculateHighScorePosition(score) + " on the high score table!");
    }

    public static int calculateHighScorePosition(int score){
        if (score > 1000) {
            return 1;
        } else if (score > 500 && score <= 1000) {
            return 2;
        } else if (score > 100 && score <=500) {
            return 3;
        } else {
            return 4;
        }
    }
}
