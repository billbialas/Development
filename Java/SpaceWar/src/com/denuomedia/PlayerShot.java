package com.denuomedia;

import java.awt.*;
import java.util.LinkedList;

/**
 * Created by wbialas on 4/27/2016.
 */
public class PlayerShot extends GameObject {
    private Handler handler;

    public PlayerShot(float x, float y, ObjectId id, int velX, Handler handler) {
        super(x, y, id);
        this.velX=velX;
        this.handler=handler;
    }

    @Override
    public void tick(LinkedList<GameObject> object) {
        x += velX;
        collision(object);

    }

    @Override
    public void render(Graphics g) {
        g.setColor(Color.RED);
        g.fillRect((int) x, (int) y, 32, 4);


    }

    @Override
    public Rectangle getBounds() {
        return new Rectangle((int) x, (int) y, 32, 4);

    }

    private void collision(LinkedList<GameObject> object){
        for (int i=0; i<object.size();i++) {
            GameObject tempObject = object.get(i);
            if (tempObject.getId() == ObjectId.Block){
                if (getBounds().intersects(tempObject.getBounds())){
                  handler.removeObject(PlayerShot.this);

                }
            }
            // tempObject.tick(object);
        }
    }
}
