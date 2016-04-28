package com.denuomedia;

import java.awt.*;
import java.awt.image.BufferStrategy;
import java.awt.image.BufferedImage;

public class Game  extends Canvas implements Runnable {

    //public static final int WIDTH = 800, HEIGHT=WIDTH/12*9;
    public static int WIDTH=800, HEIGHT=600;
    private Thread thread;
    boolean running = false;
    private Handler handler;
    private Camera cam;
    private BufferedImage level= null;
    private Stars stars;


    public Game() {

        BufferdImageLoader loader = new BufferdImageLoader();
        level = loader.loadImage("/level1.png");

        //Init handler
        handler = new Handler();
        cam = new Camera(0,0);
        stars = new Stars();
        loadImageLevel(level);

        //Add listner for keyboard input
        //This only has to be instantiate once
        this.addKeyListener(new KeyInput(handler));

        //Create new window


        //Create new Objects
        //handler.addObject(new Player(300,300,ObjectId.Player, handler));
       // handler.addObject(new BasicEnemy(100,300,ObjectId.BasicEnemy));
       // handler.addObject(new PlayerShip(300, 300, handler, ObjectId.PlayerShip));
       // handler.createLevel();
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
        for (int i=0; i<handler.object.size();i++) {
            if (handler.object.get(i).getId() == ObjectId.PlayerShip){
                cam.tick(handler.object.get(i));
                }
            }
        }


    private void render(){
        //Buffer strategy
        BufferStrategy bs = this.getBufferStrategy();
        if (bs == null){
            this.createBufferStrategy(3);
            return;
        }

        Graphics g = bs.getDrawGraphics();
        Graphics2D g2d = (Graphics2D) g;
        g.setColor(Color.BLACK);
        g.fillRect(0,0,WIDTH,HEIGHT);

        g2d.translate(cam.getX(), cam.getY());

        //Handler does rendering for all game objects
        stars.drawStars((Graphics2D) g, (int) -cam.getX(), (int) -cam.getY(),1);
        handler.render(g);

        g2d.translate(-cam.getX(), -cam.getY());

        g.dispose();
        bs.show();
    }

    public static float clamp(float var, float min, float max){
        //Used to lock down and keep objects on screen
        if(var >= max){
            return var=max;

        }else if (var <=min){
            return var=min;
        }else {
            return var;
        }

    }

    private void loadImageLevel(BufferedImage image){
        int w= image.getWidth();
        int h = image.getHeight();

        for (int xx=0; xx< h; xx++){
            for (int yy=0;yy<w;yy++){
                int pixel = image.getRGB(xx,yy);
                int red = (pixel >> 16) & 0xff;
                int green= (pixel >>8) & 0xff;
                int blue = (pixel) & 0xff;

                if (red == 255 && green==255 & blue == 255){
                    handler.addObject(new Block(xx*16,yy*16,ObjectId.Block));
                }
                if (red == 0 && green==0 & blue == 255){
                    handler.addObject(new PlayerShip(xx*32,yy*32,handler,ObjectId.PlayerShip));
                }


            }
        }

    }

    public static void main(String[] args) {
        //Create a new Game Object
        new Window(WIDTH, HEIGHT, "Game Template", new Game());
        //new Game();



    }
}
