package com.otbmedia.Controller;

import com.otbmedia.Main;
import com.otbmedia.Model.Board;
import com.otbmedia.Model.Computer;
import com.otbmedia.Model.Player;
import com.otbmedia.Model.Coin;
import javafx.beans.property.BooleanProperty;
import javafx.beans.property.SimpleBooleanProperty;
import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.HBox;

public class MainWindowController {

    private Main application;
    private Player player;
    private Board playboard;
    private Computer computer;
    private Coin coin;

    //Views
    @FXML private Button btnNewGame;
    @FXML private Button btnMarkerX;
    @FXML private Button btnMarkerO;
    @FXML private HBox hboxNewGame;
    @FXML private HBox hboxMarkerChoice;
    @FXML private HBox hboxCoinFlip;



    public void setMain(Main application) {
        this.application = application;
        //newGame();
       // handleButton();
    }

    public void handleButton(){


    }
    public void newGame(){
        hboxNewGame.setVisible(false);
        hboxMarkerChoice.setVisible(true);
        playboard = new Board();
        playboard.initPlayingBoard();
        //System.out.println(player.getPlayerName());
    }

    public void markerChoice(){
        hboxMarkerChoice.setVisible(false);
        hboxCoinFlip.setVisible(true);
        player = new Player("Human",0,"X");
        Computer computer = new Computer("O");

    }
    public void flipCoin(){
        coin = new Coin();
        coin.flipCoin();
        System.out.println(coin.getFlipResult());

    }


}