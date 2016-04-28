package com.denuomedia;

import java.awt.*;

/**
 * Created by wbialas on 4/19/2016.
 */
public class Player extends GameObject {

    Handler handler;
    public Player(int x, int y, ObjectId id, Handler handler) {
        super(x, y, id);
        this.handler=handler;
    }

    @Override
    public void tick() {
        x += velX;
        y += velY;

        //x= Game.clamp(x,0,Game.WIDTH-39);
        //y= Game.clamp(y,0,Game.HEIGHT-58);

        collision();
    }
    private void collision(){
        for (int i=0; i<handler.object.size();i++) {
            GameObject tempObject = handler.object.get(i);
            if (tempObject.getId() == ObjectId.BasicEnemy){
                if (getBounds().intersects(tempObject.getBounds())){

                }
                //Collision has occured
            }
        }

    }

    @Override
    public void render(Graphics g) {

        //Remove
       // Graphics2D g2d = (Graphics2D) g;
       // g.setColor(Color.BLUE);
       // g2d.draw(getBounds());
        g.setColor(Color.white);
        g.fillRect(x, y, 32, 32);

    }

    @Override
    public Rectangle getBounds() {
        return new Rectangle(x,y,32,32);
    }
}
