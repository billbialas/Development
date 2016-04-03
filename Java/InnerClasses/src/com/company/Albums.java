package com.company;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;

/**
 * Created by wbialas on 3/5/2016.
 *
 * Revised the program to use an innner class
 */
public class Albums {
    private String albumName;
    private String artist;
    //Changed from an Arraylist to a class object
    private SongList song;

    public Albums(String albumName, String artist) {
        this.albumName = albumName;
        this.artist = artist;
        //Init as sonelist class
        this.song = new SongList();
    }

    //This was modified to use the Songlist class method
    public boolean addNewSong(String songName, double songDuration) {
        return this.song.add(new Songs(songName,songDuration));
    }

    //Changed to use the .add method of the innder Songlist class
    public boolean addToPlayList(int trackNumber, LinkedList<Songs> playList) {
        Songs checkedSong= this.song.findSong(trackNumber);
        if (checkedSong !=null) {
            return playList.add(checkedSong);
        }
        System.out.println("This album does not have a track " + trackNumber);
        return false;
    }


    public boolean addToPlayList(String songName, LinkedList<Songs> playList) {
        //uses the findsong method of the inner class Songlist
        Songs checkedSong = song.findSong(songName);
        if (checkedSong != null) {
            //System.out.println("Song " + songName +" Added");
            playList.add(checkedSong);
            return true;

        }
        System.out.println("Song " + songName + " not in this album");
        return false;


    }

    public boolean removeFromPlayList(String songName, LinkedList<Songs> playList) {
        //uses the findsong method of the inner class Songlist
        Songs checkedSong = song.findSong(songName);
        if (checkedSong != null) {
            //System.out.println("Song " + songName +" Added");
            playList.remove(checkedSong);
            return true;

        }
        System.out.println("Song " + songName + " not in this album");
        return false;

    }

    //This is the inner class called SongList
    private class SongList {
        private List<Songs> songsA;

        public SongList() {
            this.songsA = new ArrayList<Songs>();

        }

        public boolean add(Songs song) {
            if (songsA.contains(song)) {
                return false;
            }
            return this.songsA.add(song);
        }

        private Songs findSong(String songName) {
            //Another way to loop through the array
            for (Songs checkedSong : this.songsA) {
                if (checkedSong.getSongName().equals(songName)) {
                    return checkedSong;
                }
            }
            return null;
        }

        public Songs findSong(int trackNumber) {
            int index = trackNumber - 1;
            if ((index > 0) && (index < songsA.size())) {
                return songsA.get(index);
            }
            return null;
        }



    }

}

