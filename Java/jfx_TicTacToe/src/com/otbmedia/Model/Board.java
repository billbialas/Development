package com.otbmedia.Model;

import java.util.ArrayList;

/**
 * Created by wbialas on 3/31/2016.
 */
public class Board {
    private ArrayList<Integer> boardSpots = new ArrayList<Integer>();

    //reset the board
    public void initPlayingBoard(){
        for (int i=0; i < boardSpots.size();i++){
            boardSpots.add(i,0);
        }
    }

    public void placeMarker(int spot, int marker){

    }

}
