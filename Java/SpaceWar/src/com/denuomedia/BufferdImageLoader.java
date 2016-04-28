package com.denuomedia;

import javax.imageio.ImageIO;
import java.awt.image.BufferedImage;
import java.io.IOException;

/**
 * Created by wbialas on 4/26/2016.
 */
public class BufferdImageLoader {

    private BufferedImage image;

    public BufferedImage loadImage(String path){

        try {
            image = ImageIO.read(getClass().getResource(path));
        } catch (IOException e){

        }
        return image;
    }

}
