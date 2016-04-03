package com.otbmedia.Model;

import java.util.Random;

/**
 * Created by wbialas on 4/1/2016.
 * Heads =1
 * Tails =0
 */
public class Coin {
    private int flipResult;


    public void flipCoin(){
        Random rand = new Random();
        this.flipResult = rand.nextInt(2);
    }

//    public int getFlipResult() {
//        return flipResult;
//    }

    public String getFlipResult(){
        if (flipResult==0){
            return "Tails";
        }
        return "Heads";
    }
}
