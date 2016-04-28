package com.otbmedia.Model;

import java.util.ArrayList;
import java.util.Random;

/**
 * Created by wbialas on 3/31/2016.
 */
public class Board {
    private ArrayList<Integer> boardSpots = new ArrayList<Integer>();

    //reset the board
    public void initPlayingBoard() {
        for (int i = 0; i < 9; i++) {
            boardSpots.add(i, 0);
            //System.out.println(boardSpots.get(i));

        }

    }

    public void placeMarker(int spot, int marker) {
        //System.out.println(spot);
        //System.out.println(marker);
        boardSpots.set(spot, marker);
    }

    public void showBoardStatus() {
        for (int i = 0; i < boardSpots.size(); i++) {
            //System.out.print(boardSpots.get(i).toString() + " ");
        }
    }

    public boolean checkForWin(int marker) {
        int boardMarker = marker;
        if ((boardSpots.get(0) + boardSpots.get(1) + boardSpots.get(2) == 3 * boardMarker)
                || (boardSpots.get(0) + boardSpots.get(4) + boardSpots.get(8) == 3 * boardMarker)
                || (boardSpots.get(0) + boardSpots.get(3) + boardSpots.get(6) == 3 * boardMarker)

                || (boardSpots.get(2) + boardSpots.get(4) + boardSpots.get(6) == 3 * boardMarker)
                || (boardSpots.get(2) + boardSpots.get(5) + boardSpots.get(8) == 3 * boardMarker)

                || (boardSpots.get(6) + boardSpots.get(7) + boardSpots.get(8) == 3 * boardMarker)

                || (boardSpots.get(1) + boardSpots.get(4) + boardSpots.get(7) == 3 * boardMarker)
                || (boardSpots.get(3) + boardSpots.get(4) + boardSpots.get(5) == 3 * boardMarker)

                ) {
            return true;
        } else {
            return false;
        }


    }

    public int scanBestMove() {
        int compMarker = -1;
        int playerMarker = 1;
        int winMove;
        int blockMove;
        int bestMove=-1;
        winMove=scanWinBlock(compMarker);
        blockMove=scanWinBlock(playerMarker);
        Random rand = new Random();
        bestMove=bestMove();
        System.out.println(winMove + " " + blockMove + " " + bestMove);
        //pause();
        if (winMove>-1){
            return winMove;
        } else if (blockMove>-1){
            return blockMove;
        } else {
            return bestMove;
        }

    }

    private int scanWinBlock(int marker){
        int chkMarker = marker + marker;
        int bestMove = -1;
        //Corners are array 0,2,6,8
        //Check spots 0->1,2 | 0->4,8 | 0-> 3,6
        //Check spots 2->1,0 | 2->4,6 | 2-> 5,8
        //Check spots 6->3,0 | 6->4,2 | 6-> 7,8
        //Check spots 8->7,6 | 8->4,0 | 8-> 5,2
        //Sides are array 1,3,5,7
        //Check spots 1-> 4,7 | 3-> 4,5 | 5-> 4,3 7-> 4,1
        //Middle
        //Check spots 4->0,8 | 4->8,0 | 4-> 2,6 | 4-> 6,2
        //Check spots 4->1,7 | 4->7,1 | 4-> 3,5 | 4-> 5,3

        //Check 0
        if ((boardSpots.get(0) + boardSpots.get(1) == chkMarker) && boardSpots.get(2) == 0) {
            bestMove = 2;
        }
        if ((boardSpots.get(0) + boardSpots.get(4) == chkMarker) && boardSpots.get(8) == 0) {
            bestMove = 8;
        }
        if ((boardSpots.get(0) + boardSpots.get(3) == chkMarker) && boardSpots.get(6) == 0) {
            bestMove =6;
        }


        //Check 2
        if ((boardSpots.get(2) + boardSpots.get(1) == chkMarker) && boardSpots.get(0) == 0) {
            bestMove = 0;
        }
        if ((boardSpots.get(2) + boardSpots.get(4) == chkMarker) && boardSpots.get(6) == 0) {
            bestMove = 6;
        }
        if ((boardSpots.get(2) + boardSpots.get(5) == chkMarker) && boardSpots.get(8) == 0) {
            bestMove = 8;
        }
        //Check 6
        if ((boardSpots.get(6) + boardSpots.get(3) == chkMarker) && boardSpots.get(0) == 0) {
            bestMove = 0;
        }
        if ((boardSpots.get(6) + boardSpots.get(4) == chkMarker) && boardSpots.get(2) == 0) {
            bestMove = 2;
        }
        if ((boardSpots.get(6) + boardSpots.get(7) == chkMarker) && boardSpots.get(8) == 0) {
            bestMove =8;
        }
        //Check 8
        if ((boardSpots.get(8) + boardSpots.get(5) == chkMarker) && boardSpots.get(2) == 0) {
            bestMove = 2;
        }
        if ((boardSpots.get(8) + boardSpots.get(4) == chkMarker) && boardSpots.get(0) == 0) {
            bestMove = 0;
        }
        if ((boardSpots.get(8) + boardSpots.get(7) == chkMarker) && boardSpots.get(6) == 0) {
            bestMove =6;
        }
        //Check 1,3,5,7
        if ((boardSpots.get(1) + boardSpots.get(4) == chkMarker) && boardSpots.get(7) == 0) {
            bestMove = 7;
        }
        if ((boardSpots.get(3) + boardSpots.get(4) == chkMarker) && boardSpots.get(5) == 0) {
            bestMove = 5;
        }
        if ((boardSpots.get(5) + boardSpots.get(4) == chkMarker) && boardSpots.get(3) == 0) {
            bestMove = 3;
        }
        if ((boardSpots.get(7) + boardSpots.get(4) == chkMarker) && boardSpots.get(1) == 0) {
            bestMove = 1;
        }
        //Check 4
        if ((boardSpots.get(4) + boardSpots.get(0) == chkMarker) && boardSpots.get(8) == 0) {
            bestMove =8;
        }
        if ((boardSpots.get(4) + boardSpots.get(8) == chkMarker) && boardSpots.get(0) == 0) {
            bestMove = 0;
        }
        if ((boardSpots.get(4) + boardSpots.get(2) == chkMarker) && boardSpots.get(6) == 0) {
            bestMove = 6;
        }
        if ((boardSpots.get(4) + boardSpots.get(6) == chkMarker) && boardSpots.get(2) == 0) {
            bestMove =2;
        }
        if ((boardSpots.get(4) + boardSpots.get(1) == chkMarker) && boardSpots.get(7) == 0) {
            bestMove = 7;
        }
        if ((boardSpots.get(4) + boardSpots.get(7) == chkMarker) && boardSpots.get(1) == 0) {
            bestMove = 1;
        }
        if ((boardSpots.get(4) + boardSpots.get(3) == chkMarker) && boardSpots.get(5) == 0) {
            bestMove = 5;
        }
        if ((boardSpots.get(4) + boardSpots.get(5) == chkMarker) && boardSpots.get(3) == 0) {
            bestMove = 3;
        }

        //Middle Space Empty
        if ((boardSpots.get(0) + boardSpots.get(2) == chkMarker) && boardSpots.get(1) == 0) {
            bestMove =1;
        }

        if ((boardSpots.get(6) + boardSpots.get(8) == chkMarker) && boardSpots.get(7) == 0) {
            bestMove =7;
        }

        if ((boardSpots.get(0) + boardSpots.get(6) == chkMarker) && boardSpots.get(3) == 0) {
            bestMove =3;
        }

        if ((boardSpots.get(2) + boardSpots.get(8) == chkMarker) && boardSpots.get(5) == 0) {
            bestMove =5;
        }

        return bestMove;
    }

    private int bestMove(){
        int bestMove=-1;

        if (boardSpots.get(0) == -1 && boardSpots.get(1) == 0) {
            bestMove = 1;
        }
        if (boardSpots.get(0) == -1 && boardSpots.get(4) == 0) {
            bestMove = 4;
        }
        if (boardSpots.get(0) == -1 && boardSpots.get(6) == 0) {
            bestMove = 6;
        }
        if (boardSpots.get(1) == -1 && boardSpots.get(0) == 0) {
            bestMove = 0;
        }
        if (boardSpots.get(1) == -1 && boardSpots.get(4) == 0) {
            bestMove = 4;
        }
        if (boardSpots.get(1) == -1 && boardSpots.get(2) == 0) {
            bestMove = 2;
        }
        if (boardSpots.get(2) == -1 && boardSpots.get(1) == 0) {
            bestMove = 1;
        }
        if (boardSpots.get(2) == -1 && boardSpots.get(4) == 0) {
            bestMove = 4;
        }
        if (boardSpots.get(2) == -1 && boardSpots.get(5) == 0) {
            bestMove = 5;
        }

        if (boardSpots.get(3) == -1 && boardSpots.get(0) == 0) {
            bestMove = 0;
        }
        if (boardSpots.get(3) == -1 && boardSpots.get(4) == 0) {
            bestMove = 4;
        }
        if (boardSpots.get(3) == -1 && boardSpots.get(6) == 0) {
            bestMove = 6;
        }

        if (boardSpots.get(5) == -1 && boardSpots.get(2) == 0) {
            bestMove = 1;
        }
        if (boardSpots.get(5) == -1 && boardSpots.get(4) == 0) {
            bestMove =4;
        }
        if (boardSpots.get(5) == -1 && boardSpots.get(8) == 0) {
            bestMove = 8;
        }

        if (boardSpots.get(6) == -1 && boardSpots.get(3) == 0) {
            bestMove = 3;
        }
        if (boardSpots.get(6) == -1 && boardSpots.get(4) == 0) {
            bestMove = 4;
        }
        if (boardSpots.get(6) == -1 && boardSpots.get(7) == 0) {
            bestMove = 6;
        }

        if (boardSpots.get(7) == -1 && boardSpots.get(6) == 0) {
            bestMove = 6;
        }
        if (boardSpots.get(7) == -1 && boardSpots.get(4) == 0) {
            bestMove =4;
        }
        if (boardSpots.get(7) == -1 && boardSpots.get(8) == 0) {
            bestMove = 8;
        }

        if (boardSpots.get(8) == -1 && boardSpots.get(7) == 0) {
            bestMove = 7;
        }
        if (boardSpots.get(8) == -1 && boardSpots.get(4) == 0) {
            bestMove =4;
        }
        if (boardSpots.get(8) == -1 && boardSpots.get(5) == 0) {
            bestMove = 5;
        }

        if (boardSpots.get(4) == -1 && boardSpots.get(0) == 0) {
            bestMove = 0;
        }
        if (boardSpots.get(4) == -1 && boardSpots.get(1) == 0) {
            bestMove = 1;
        }
        if (boardSpots.get(4) == -1 && boardSpots.get(2) == 0) {
            bestMove = 2;
        }
        if (boardSpots.get(4) == -1 && boardSpots.get(3) == 0) {
            bestMove = 3;
        }
        if (boardSpots.get(4) == -1 && boardSpots.get(5) == 0) {
            bestMove = 5;
        }
        if (boardSpots.get(4) == -1 && boardSpots.get(6) == 0) {
            bestMove = 5;
        }

        if (boardSpots.get(4) == -1 && boardSpots.get(7) == 0) {
            bestMove = 7;
        }
        if (boardSpots.get(4) == -1 && boardSpots.get(8) == 0) {
            bestMove = 8;
        }

        //Check corners
        if (boardSpots.get(4) == 1 && boardSpots.get(0) == 0 && boardSpots.get(8) < 1) {
            bestMove = 0;
        }
        // Need to finsih remainder










        if (boardSpots.get(4) == -1 && boardSpots.get(0) == 0 && boardSpots.get(8) < 1) {
            bestMove = 0;
        }

        if (boardSpots.get(4) == -1 && boardSpots.get(8) == 0 && boardSpots.get(0) < 1) {
            bestMove = 8;
        }

        if (boardSpots.get(4) == -1 && boardSpots.get(2) == 0 && boardSpots.get(6) < 1) {
            bestMove = 2;
        }

        if (boardSpots.get(4) == -1 && boardSpots.get(6) == 0 && boardSpots.get(2) < 1) {
            bestMove = 6;
        }

        //Pattern Moves
        if (boardSpots.get(4) == -1 && boardSpots.get(6) == -1 && boardSpots.get(7) <1 && boardSpots.get(8) ==0 ) {
            bestMove = 8;
        }

        if (boardSpots.get(4) == -1 && boardSpots.get(8) == -1 && boardSpots.get(7) <1 && boardSpots.get(6) ==0 ) {
            bestMove = 6;
        }

        if (boardSpots.get(4) == -1 && boardSpots.get(0) == -1 && boardSpots.get(1) <1 && boardSpots.get(2) ==0 ) {
            bestMove = 2;
        }

        if (boardSpots.get(4) == -1 && boardSpots.get(2) == -1 && boardSpots.get(1) <1 && boardSpots.get(0) ==0 ) {
            bestMove = 0;
        }


        if (boardSpots.get(4) == -1 && boardSpots.get(6) == -1 && boardSpots.get(3) <1 && boardSpots.get(0) ==0 ) {
            bestMove = 0;
        }
        if (boardSpots.get(4) == -1 && boardSpots.get(0) == -1 && boardSpots.get(3) <1 && boardSpots.get(6) ==0 ) {
            bestMove = 6;
        }

        if (boardSpots.get(4) == -1 && boardSpots.get(8) == -1 && boardSpots.get(5) <1 && boardSpots.get(2) ==0 ) {
            bestMove = 2;
        }
        if (boardSpots.get(4) == -1 && boardSpots.get(2) == -1 && boardSpots.get(5) <1 && boardSpots.get(8) ==0 ) {
            bestMove = 8;
        }

        //
        if (boardSpots.get(4) == -1 && boardSpots.get(6) == -1 && boardSpots.get(3) ==0  && boardSpots.get(5) ==0 ) {
            bestMove = 3;
        }

        if (boardSpots.get(4) == -1 && boardSpots.get(0) == -1 && boardSpots.get(3) ==0  && boardSpots.get(5) ==0 ) {
            bestMove = 3;
        }

        if (boardSpots.get(4) == -1 && boardSpots.get(8) == -1 && boardSpots.get(3) ==0  && boardSpots.get(5) ==0 ) {
            bestMove = 5;
        }

        if (boardSpots.get(4) == -1 && boardSpots.get(2) == -1 && boardSpots.get(3) ==0  && boardSpots.get(5) ==0 ) {
            bestMove = 5;
        }

        return bestMove;
    }
    private void pause(){
        try {
            Thread.sleep(1000);                 //1000 milliseconds is one second.
        } catch(InterruptedException ex) {
            Thread.currentThread().interrupt();
        }
    }
}
