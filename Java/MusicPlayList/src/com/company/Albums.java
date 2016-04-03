package com.company;

import java.util.ArrayList;
import java.util.LinkedList;

/**
 * Created by wbialas on 3/5/2016.
 */
public class Albums {
    private String albumName;
    private String artist;
    private ArrayList<Songs> song;

    public Albums(String albumName, String artist) {
        this.albumName = albumName;
        this.artist = artist;
        this.song = new ArrayList<Songs>();
    }

    public boolean addNewSong(String songName, double songDuration) {
        if (findSong(songName) == null) {
            this.song.add(new Songs(songName,songDuration));
            return true;
        }


        System.out.println("Song already exists");
        return false;


    }

    private Songs findSong(String songName){
        //Another way to loop through the array
        for (Songs checkedSong: this.song){
            if (checkedSong.getSongName().equals(songName)){
                return checkedSong;
            }
        }
        return null;
    }

    public boolean addToPlayList(int trackNumber, LinkedList<Songs> playList) {
        int index = trackNumber - 1;
        if ((index > 0) && (index <= this.song.size())) {
            playList.add(this.song.get(index));
            return true;
        }
        System.out.println("This album does not have a track "+ trackNumber);
        return false;
    }

    public boolean addToPlayList(String songName, LinkedList<Songs> playList){
        Songs checkedSong = findSong(songName);
        if (checkedSong != null){
            //System.out.println("Song " + songName +" Added");
            playList.add(checkedSong);
            return true;

        }
        System.out.println("Song " + songName +" not in this album");
        return false;


    }

    public boolean removeFromPlayList(String songName, LinkedList<Songs> playList){
        Songs checkedSong = findSong(songName);
        if (checkedSong != null){
            //System.out.println("Song " + songName +" Added");
            playList.remove(checkedSong);
            return true;

        }
        System.out.println("Song " + songName +" not in this album");
        return false;


    }

}
