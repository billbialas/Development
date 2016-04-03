package sample;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.text.Text;
import javafx.stage.Stage;

public class Controller2 {
    @FXML private Text actiontarget;
//    @FXML private Button myButton;
    Main application;

    @FXML protected void handleSubmitButtonAction2(ActionEvent event) throws Exception {
        Stage stage;
        Parent root;

        actiontarget.setText("Sign in button pressed");
        stage = new Stage();
        root = FXMLLoader.load(getClass().getResource("sample2.fxml"));
        stage.setScene(new Scene(root));
        stage.setTitle("My modal window");
        stage.showAndWait();
    }
}
