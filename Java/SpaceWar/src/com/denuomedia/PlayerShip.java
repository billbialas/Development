package com.denuomedia;

import javax.swing.plaf.ColorUIResource;
import java.awt.*;
import java.util.LinkedList;

/**
 * Created by wbialas on 4/25/2016.
 */
public class PlayerShip extends GameObject {

    private float width=64, height=32;
    private Handler handler;

    public PlayerShip(float x, float y, Handler handler, ObjectId id) {
        super(x, y, id);
        this.handler=handler;
    }

    @Override
    public void tick(LinkedList<GameObject> object) {
        x += velX;
        y += velY;
        collision(object);
    }

    @Override
    public void render(Graphics g) {
        g.setColor(Color.blue);
        g.fillRect((int) x, (int) y, (int) width, (int) height);

        Graphics2D g2d = (Graphics2D) g;
        g.setColor(Color.RED);
        g2d.draw(getBounds());
        g2d.draw(getBoundsRight());
        g2d.draw(getBoundsLeft());
        g2d.draw(getBoundsTop());

    }

    @Override
    public Rectangle getBounds() {
        return new Rectangle((int) ((int) x+(width/2)-(width/2)/2), (int) ((int) y+(height/2)), (int) width/2, (int) height/2);

    }
    public Rectangle getBoundsTop() {
        return new Rectangle((int) ((int) x+(width/2)-(width/2)/2), (int) y, (int) width/2, (int) height/2);

    }
    public Rectangle getBoundsRight() {
        return new Rectangle((int) ((int) x+width-5), (int) y+5, (int) 5, (int) height-10);

    }
    public Rectangle getBoundsLeft() {
        return new Rectangle((int) x, (int) y+5, (int) 5, (int) height-10);

    }


    private void collision(LinkedList<GameObject> object){
        for (int i=0; i<handler.object.size();i++) {
            GameObject tempObject = handler.object.get(i);
            if (tempObject.getId() == ObjectId.Block){
                if (getBounds().intersects(tempObject.getBounds())){
                    y = tempObject.getY()-height;
                    velY=0;
                }
            }
           // tempObject.tick(object);
        }
    }

}
