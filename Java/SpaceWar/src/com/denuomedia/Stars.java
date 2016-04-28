package com.denuomedia;

import java.awt.*;

/**
 * Created by wbialas on 4/26/2016.
 */
public class Stars {

    public static final int STAR_SEED = 0x9d2c5680;
    //0x9d2c5680

    public static final int STAR_TILE_SIZE = 128;

    public void drawStars(Graphics2D g, int xoff, int yoff, int starscale) {
        int size = STAR_TILE_SIZE / starscale;
        int w = Game.WIDTH*12;
        int h = Game.HEIGHT;

    /* Top-left tile's top-left position. */
        int sx = ((xoff - w/2) / size) * size - size;
        int sy = ((yoff - h/2) / size) * size - size;

    /* Draw each tile currently in view. */
        for (int i = sx; i <= w + sx + size * 3; i += size) {
            for (int j = sy; j <= h + sy + size * 3; j += size) {
                int hash = mix(STAR_SEED, i, j);
                for (int n = 0; n < 3; n++) {
                    int px = (hash % size) + (i - xoff);
                    hash >>= 3;
                    int py = (hash % size) + (j - yoff);
                    hash >>= 3;
                    g.setColor(Color.white);
                    g.drawLine(px, py, px, py);
                    //g.drawLine(px+1, py, px+1, py);
                }
            }
        }
    }

    private static int mix(int a, int b, int c) {
        a=a-b;  a=a-c;  a=a^(c >>> 13);
        b=b-c;  b=b-a;  b=b^(a << 8);
        c=c-a;  c=c-b;  c=c^(b >>> 13);
        a=a-b;  a=a-c;  a=a^(c >>> 12);
        b=b-c;  b=b-a;  b=b^(a << 16);
        c=c-a;  c=c-b;  c=c^(b >>> 5);
        a=a-b;  a=a-c;  a=a^(c >>> 3);
        b=b-c;  b=b-a;  b=b^(a << 10);
        c=c-a;  c=c-b;  c=c^(b >>> 15);
        return c;
    }
}
