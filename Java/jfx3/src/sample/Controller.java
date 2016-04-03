package sample;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.text.Text;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Modality;
import javafx.stage.Stage;

public class Controller {
    @FXML private Text actiontarget;

    Main application;

    @FXML protected void handleSubmitButtonAction(ActionEvent event) throws Exception {
        Stage stage;
        Parent root;

        actiontarget.setText("Sign in button pressed");
        stage = new Stage();
        stage.initModality(Modality.APPLICATION_MODAL);
//        stage.initOwner(btn1.getscene().getwindow());
        stage.showAndWait();
        root = FXMLLoader.load(getClass().getResource("sample2.fxml"));
        stage.setScene(new Scene(root));
        stage.setTitle("My modal window");
        stage.showAndWait();
    }
}
