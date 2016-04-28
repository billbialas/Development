package com.denuomedia;

import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;

/**
 * Created by wbialas on 4/19/2016.
 */
public class KeyInput extends KeyAdapter {

    private Handler handler;
    private boolean[] keyDown = new boolean[4];
    private int speed = 5;

    public KeyInput(Handler handler) {
        this.handler = handler;
        keyDown[0]=false;
        keyDown[1]=false;
        keyDown[2]=false;
        keyDown[3]=false;
    }

    public void keyPressed(KeyEvent e){
        int key = e.getKeyCode();
        int shots = 0;
        for (int i=0;i < handler.object.size(); i++){
            GameObject tempObject = handler.object.get(i);

            if (tempObject.getId()== ObjectId.PlayerShip){
                //key events for player 1
                if (key == KeyEvent.VK_UP){
                    tempObject.setVelY(speed*-1);
                    keyDown[0]=true;
                }
                if (key == KeyEvent.VK_DOWN){
                    tempObject.setVelY(speed);
                    keyDown[1]=true;
                }
                if (key == KeyEvent.VK_RIGHT){
                    tempObject.setVelX(speed);
                    keyDown[2]=true;
                    tempObject.facing=1;
                }
                if (key == KeyEvent.VK_LEFT){
                    tempObject.setVelX(speed*-1);
                    keyDown[3]=true;
                    tempObject.facing=-1;
                }
                if (key == KeyEvent.VK_SPACE){

                    for (int j=0;j < handler.object.size(); j++) {
                        GameObject tempObject2 = handler.object.get(j);
                        if (tempObject2.getId() == ObjectId.PlayerShot) {
                            shots ++;
                        }
                    }
                    handler.addObject(new PlayerShot(tempObject.getX()+32, tempObject.getY()+12,ObjectId.PlayerShot, tempObject.getFacing() * 16,handler) );
//                    System.out.println(shots);
//                    if (shots <3){
//
//
//                    }
                      }

            }
        }

        if (key == KeyEvent.VK_ESCAPE) System.exit(1);

    }

    public void keyReleased(KeyEvent e){
        int key = e.getKeyCode();
        for (int i=0;i < handler.object.size(); i++){
            GameObject tempObject = handler.object.get(i);

            if (tempObject.getId()== ObjectId.PlayerShip){
                //key events for player 1

                if (key == KeyEvent.VK_UP){
                    //tempObject.setVelY(0);
                    keyDown[0]=false;
                }
                if (key == KeyEvent.VK_DOWN){
                    //tempObject.setVelY(0);
                    keyDown[1]=false;
                }
                if (key == KeyEvent.VK_RIGHT){
                    //tempObject.setVelX(0);
                    keyDown[2]=false;
                }
                if (key == KeyEvent.VK_LEFT){
                    //tempObject.setVelX(0);
                    keyDown[3]=false;
                }

                if(!keyDown[0] && !keyDown[1]){
                    tempObject.setVelY(0);
                }
                if(!keyDown[2] && !keyDown[3]){
                    tempObject.setVelX(0);
                }
            }
        }
    }

}
