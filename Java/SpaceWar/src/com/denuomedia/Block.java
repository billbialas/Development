package com.denuomedia;

import java.awt.*;
import java.util.LinkedList;

/**
 * Created by wbialas on 4/25/2016.
 */
public class Block extends GameObject {

    public Block(float x, float y, ObjectId id) {
        super(x, y, id);
    }

    @Override
    public void tick(LinkedList<GameObject> object) {

    }

    @Override
    public void render(Graphics g) {
        g.setColor(Color.white);
        g.drawRect((int) x, (int)y, 16,16);

    }

    @Override
    public Rectangle getBounds() {
        return new Rectangle((int)x,(int) y,16,16);
    }


}
