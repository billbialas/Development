package com.company;

/**
 * Created by wbialas on 3/5/2016.
 */
public class Songs {
    private String songName;
    private double songDuration;

    public Songs(String songName, double songDuration) {
        this.songName = songName;
        this.songDuration = songDuration;
    }

    public String getSongName() {
        return songName;
    }

    @Override
    public String toString() {
        return this.songName + ": " + this.songDuration;
    }
}
