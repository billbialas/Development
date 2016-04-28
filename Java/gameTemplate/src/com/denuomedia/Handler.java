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
            tempObject.tick();
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
}
