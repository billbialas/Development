package com.denuomedia;

import java.awt.*;
import java.util.LinkedList;

/**
 * Created by wbialas on 4/19/2016.
 */
public class BasicEnemy extends GameObject {

    public BasicEnemy(float x, float y, ObjectId id) {
        super(x, y, id);

        velX=5;
        velY=5;

    }

    @Override
    public void tick(LinkedList<GameObject> object) {
        x += velX;
        y += velY;

        if(y<=0 || y>=Game.HEIGHT -32) velY *= -1;
        if(x<=0 || x>=Game.WIDTH -16 ) velX *= -1;
    }

    @Override
    public void render(Graphics g) {
        g.setColor(Color.RED);
        g.fillRect((int) x, (int) y,16,16);

    }

    @Override
    public Rectangle getBounds() {
        return new Rectangle((int) x, (int)y,16,16);
    }
}
