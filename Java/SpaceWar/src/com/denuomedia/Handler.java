package com.denuomedia;

import java.awt.*;
import java.util.LinkedList;

/**
 * Created by wbialas on 4/19/2016.
 */
public class Handler {

    LinkedList<GameObject> object = new LinkedList<GameObject>();

    public void tick(){
        for (int i=0; i<object.size();i++) {
            GameObject tempObject = object.get(i);
            tempObject.tick(object);
        }
    }

    public void render(Graphics g){
        for (int i=0; i<object.size();i++) {
            GameObject tempObject = object.get(i);
            tempObject.render(g);
        }

    }

    public void addObject(GameObject object){
        this.object.add(object);
    }

    public void removeObject(GameObject object){
        this.object.remove(object);
    }

    public void createLevel(){

        for (int yy=0;yy<Game.HEIGHT+16; yy+=16){
            addObject(new Block(0,yy,ObjectId.Block));
        }
//        for (int xx=0;xx<Game.WIDTH*12; xx+=16){
//            addObject(new Block(xx,Game.HEIGHT-48,ObjectId.Block));
//        }
//        for (int xx=200;xx<600; xx+=32){
//            addObject(new Block(xx,400,ObjectId.Block));
//        }


    }
}
