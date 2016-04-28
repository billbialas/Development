package com.otbmedia.Controller;

import com.otbmedia.Main;
import com.otbmedia.Model.Board;
import com.otbmedia.Model.Computer;
import com.otbmedia.Model.Player;
import com.otbmedia.Model.Coin;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.CheckBox;
import javafx.scene.control.Label;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.HBox;

public class MainWindowController {

    private Main application;
    private Player player;
    private Board playboard;
    private Computer computer;
    private Coin coin;
    private boolean computerFirst;

    final int playerInternalMarker=1, computerInternalMarker=-1;

    //Views
    @FXML private Button btnNewGame,btnMarkerX,btnMarkerO,btnCoinFlip;
    @FXML private Label lblSpot1,lblSpot2,lblSpot3,lblSpot4,lblSpot5,lblSpot6,lblSpot7,lblSpot8,lblSpot9,lblMessages;
    @FXML private HBox hboxNewGame,hboxMarkerChoice,hboxCoinFlip,hboxUserMessages;
    @FXML private CheckBox chkHeads,chkTails;

    public void setMain(Main application) {
        this.application = application;
        hboxNewGame.setVisible(true);

    }

    @FXML
    public void newGame(){
        hboxNewGame.setVisible(false);
        hboxMarkerChoice.setVisible(true);
        hboxUserMessages.setVisible(false);
        playboard = new Board();
        playboard.initPlayingBoard();
        player = new Player();
        computer = new Computer();
    }

    @FXML
    public void markerChoice(ActionEvent event){
        hboxMarkerChoice.setVisible(false);
        hboxCoinFlip.setVisible(true);
        btnCoinFlip.setDisable(true);

        if (event.getSource().equals(btnMarkerX)){
            player.setPlayerMarker("X");
            computer.setComputerMarker("O");
        }else{
            player.setPlayerMarker("O");
            computer.setComputerMarker("X");
        }
    }

    @FXML
    public void flipCoin(){
        coin = new Coin();
        coin.flipCoin();
        String flipResult =coin.getFlipResult();
        String firstPlayer="";
        hboxUserMessages.setVisible(true);
        hboxCoinFlip.setVisible(false);
        if (flipResult=="Heads"){
            if (chkHeads.isSelected()){
                computerFirst = false;
                firstPlayer="You go first";
            } else {
                computerFirst = true;
                firstPlayer="I go first";
            }
        } else if (flipResult=="Tails") {
            if (chkTails.isSelected()){
                computerFirst = false;
                firstPlayer="You go first";
            } else {
                computerFirst = true;
                firstPlayer="I go first";
            }
        }


        lblMessages.setText("It was: " + flipResult+ "\n" + firstPlayer);

        //computerFirst=false;
        //Update display with winner
        setSpotStatus(false);
        if (computerFirst){
            computerMove(true);

        }
    }

    public void coinTossChoice(ActionEvent event){
        Boolean isSelected;
        //System.out.println(event.s));

        if (event.getSource().equals(chkHeads)){
            chkTails.setSelected(false);
            btnCoinFlip.setDisable(false);
        } else if (event.getSource().equals(chkTails)){
            chkHeads.setSelected(false);
            btnCoinFlip.setDisable(false);
        } else {
            btnCoinFlip.setDisable(true);
        }

    }

    public void setSpotStatus(Boolean status){
        lblSpot1.setDisable(status);
        lblSpot2.setDisable(status);
        lblSpot3.setDisable(status);
        lblSpot4.setDisable(status);
        lblSpot5.setDisable(status);
        lblSpot6.setDisable(status);
        lblSpot7.setDisable(status);
        lblSpot8.setDisable(status);
        lblSpot9.setDisable(status);
    }

    public void playerTurn(MouseEvent event){
        int spot=-1;
        String playerMarker = player.getPlayerMarker();
        if (event.getSource().equals(lblSpot1) ){
            lblSpot1.setText(playerMarker);
            spot=0;
            lblSpot1.setDisable(true);
        }else if (event.getSource() == lblSpot2){
            lblSpot2.setText(playerMarker);
            spot=1;
            lblSpot2.setDisable(true);
        }else if (event.getSource() == lblSpot3) {
            lblSpot3.setText(playerMarker);
            spot = 2;
            lblSpot3.setDisable(true);
        }else if (event.getSource() == lblSpot4) {
            lblSpot4.setText(playerMarker);
            spot = 3;
            lblSpot4.setDisable(true);
        }else if (event.getSource() == lblSpot5) {
            lblSpot5.setText(playerMarker);
            spot = 4;
            lblSpot5.setDisable(true);
        }else if (event.getSource() == lblSpot6) {
            lblSpot6.setText(playerMarker);
            spot = 5;
            lblSpot6.setDisable(true);
        }else if (event.getSource() == lblSpot7) {
            lblSpot7.setText(playerMarker);
            spot = 6;
            lblSpot7.setDisable(true);
        }else if (event.getSource() == lblSpot8) {
            lblSpot8.setText(playerMarker);
            spot = 7;
            lblSpot8.setDisable(true);
        }else if (event.getSource() == lblSpot9) {
            lblSpot9.setText(playerMarker);
            spot = 8;
            lblSpot9.setDisable(true);
        }
        playboard.placeMarker(spot,playerInternalMarker);
        playboard.showBoardStatus();

        if (playboard.checkForWin(playerInternalMarker)){
            endGame("Human");
        } else{
            computerMove(false);

        }
    }



    public void computerMove(boolean moveFirst) {
        int spot = -1;
        String computerMarker = computer.getComputerMarker();
        //If computer first pick middle
        //pause();
        if (moveFirst) {
            spot = 4;
            playboard.placeMarker(spot, computerInternalMarker);
            lblSpot5.setText(computerMarker);
            lblSpot5.setDisable(true);
        } else {
            spot =playboard.scanBestMove();
            System.out.println(spot);
            playboard.placeMarker(spot, computerInternalMarker);
           // pause();
            if (spot==0) {
                lblSpot1.setText(computerMarker);
                lblSpot1.setDisable(true);
            }
            if (spot==1) {
                lblSpot2.setText(computerMarker);
                lblSpot2.setDisable(true);
            }
            if (spot==2) {
                lblSpot3.setText(computerMarker);
                lblSpot3.setDisable(true);
            }
            if (spot==3) {
                lblSpot4.setText(computerMarker);
                lblSpot4.setDisable(true);
            }
            if (spot==4) {
                lblSpot5.setText(computerMarker);
                lblSpot5.setDisable(true);
            }
            if (spot==5) {
                lblSpot6.setText(computerMarker);
                lblSpot6.setDisable(true);
            }
            if (spot==6) {
                lblSpot7.setText(computerMarker);
                lblSpot7.setDisable(true);
            }
            if (spot==7) {
                lblSpot8.setText(computerMarker);
                lblSpot8.setDisable(true);
            }
            if (spot==8) {
                lblSpot9.setText(computerMarker);
                lblSpot9.setDisable(true);
            }

        }
       // playboard.showBoardStatus();
        if (playboard.checkForWin(computerInternalMarker)) {
            System.out.println("I am here");
            endGame("Computer");
        } else {
            //Update display to indicate it players turn

        }
    }


    public void endGame(String winner){
        setSpotStatus(true);
        lblMessages.setText("Winner is: " + winner);
        hboxNewGame.setVisible(true);
    }

}