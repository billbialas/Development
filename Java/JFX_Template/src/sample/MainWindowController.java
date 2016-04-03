package sample;

import com.sun.xml.internal.ws.api.FeatureConstructor;
import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;

public class MainWindowController {

    private Main application;

    //Views
    @FXML private Label lblText;
    @FXML private Button btnClickMe;
    @FXML private TextField txtText;


    public void setMain(Main application) {
        this.application = application;
    }

    public void handleButton(){
        String text = txtText.getText();
        lblText.setText(text);
        txtText.clear();
    }
}
