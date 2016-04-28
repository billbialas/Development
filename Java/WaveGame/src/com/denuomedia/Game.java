package com.denuomedia;

import java.awt.*;
import java.awt.image.BufferStrategy;

public class Game  extends Canvas implements Runnable {

    public static final int WIDTH = 800, HEIGHT=WIDTH/12*9;
    private Thread thread;
    boolean running = false;
    private Handler handler;

    public Game() {
        //Init handler
        handler = new Handler();

        //Add listner for keyboard input
        //This only has to be instantiate once
        this.addKeyListener(new KeyInput(handler));

        //Create new window
        new Window(WIDTH, HEIGHT, "Game Template", this);

        //Create new Objects
        handler.addObject(new Player(300,300,ObjectId.Player, handler));
        handler.addObject(new BasicEnemy(100,300,ObjectId.BasicEnemy));
    }

    public synchronized void start() {
        //Create and start new thread
        thread = new Thread(this);
        thread.start();
        running=true;
    }

    public synchronized void stop() {
        //Stops threads
        try {
            thread.join();
            running=false;
        }catch(Exception e){
            e.printStackTrace();
        }

    }

    public void run(){
        //Game loop
        this.requestFocus();
        long lastTime = System.nanoTime();
        double amountOfTicks = 60.0;
        double ns = 1000000000 / amountOfTicks;
        double delta =0;
        long timer = System.currentTimeMillis();
        int frames=0;
        while(running){
            long now = System.nanoTime();
            delta += (now - lastTime) / ns;
            lastTime = now;
            while (delta >= 1){
                tick();
                delta--;
            }
            if(running)
                render();
            frames++;

            if(System.currentTimeMillis()-timer>1000){
                timer += 1000;
                System.out.println("FPS: " + frames);
                frames=0;
            }

        }
        stop();

    }
    private void tick(){
        //Handler does update of all game objects
        handler.tick();

    }

    private void render(){
        //Buffer strategy
        BufferStrategy bs = this.getBufferStrategy();
        if (bs == null){
            this.createBufferStrategy(3);
            return;
        }

        Graphics g = bs.getDrawGraphics();

        g.setColor(Color.BLACK);
        g.fillRect(0,0,WIDTH,HEIGHT);

        //Handler does rendering for all game objects
        handler.render(g);

        g.dispose();
        bs.show();
    }

    public static int clamp(int var, int min, int max){
        //Used to lock down and keep objects on screen
        if(var >= max){
            return var=max;

        }else if (var <=min){
            return var=min;
        }else {
            return var;
        }

    }


    public static void main(String[] args) {
        //Create a new Game Object
        new Game();



    }
}
