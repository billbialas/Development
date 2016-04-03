package com.company;

import javafx.application.Application;
import javafx.geometry.Insets;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.ChoiceBox;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

import java.util.ArrayList;

public class Main extends Application {

    Stage window;
    Scene scene;
    Button button;
    ArrayList<String> fruits;

    @Override
    public void start(Stage primaryStage) throws Exception {
        window = primaryStage;
        window.setTitle("Bialas ChoiceBox");
        button = new Button("Click Me");
        fruits = new ArrayList<String>(3);
        fruits.add("Apples");
        fruits.add("Grapes");
        fruits.add("Oranges");

        ChoiceBox<String> choiceBox = new ChoiceBox<String>();
        choiceBox.getItems().addAll(fruits);
        choiceBox.setValue(fruits.get(0));

        button.setOnAction(e -> getChoice(choiceBox));
        choiceBox.getSelectionModel().selectedItemProperty().addListener((v, oldValue, newValue) -> processChoice(choiceBox));

        VBox layout = new VBox(10);
        layout.setPadding(new Insets(20, 20, 20, 20));
        layout.getChildren().addAll(choiceBox, button);

        scene = new Scene(layout, 300, 330);
        window.setScene(scene);
        window.show();
    }

    public static void main(String[] args) {
        launch();
    }

    private void getChoice(ChoiceBox<String> choiceBox) {
        String food = choiceBox.getValue();
        System.out.println(food);

    }
    private void processChoice(ChoiceBox<String> choiceBox) {
        String food = choiceBox.getValue();
        System.out.println(food);

    }



}
