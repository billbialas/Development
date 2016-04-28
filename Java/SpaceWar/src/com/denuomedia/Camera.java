package com.denuomedia;

/**
 * Created by wbialas on 4/25/2016.
 */
public class Camera {

    private float x,y;

    public Camera(float x, float y) {
        this.x = x;
        this.y = y;
    }

    public float getX() {
        return x;
    }

    public void setX(float x) {
        this.x = x;
    }

    public float getY() {
        return y;
    }

    public void setY(float y) {
        this.y = y;
    }

    public void tick(GameObject player){

//        float xTarg = -player.getX() + Game.WIDTH /2;
//        x += (xTarg-x)*(1);
        x = -player.getX() + Game.WIDTH/2;
        if (this.x > 0){
            x =0;

        }
//        System.out.println("X= " + this.x);
//        System.out.println("PX= " + player.getX());


        //x = -player.getX() + Game.WIDTH /2;
        // y = -player.getY() + Game.HEIGHT /2;


    }

}
