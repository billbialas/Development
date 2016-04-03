package com.company;

import com.sun.org.apache.xml.internal.utils.ListingErrorHandler;
import com.sun.org.apache.xpath.internal.SourceTree;

import java.util.*;

public class Main {

    private static ArrayList<Albums> albums = new ArrayList<Albums>();


    public static void main(String[] args) {

        LinkedList<Songs> playlist = new LinkedList<Songs>();

        Albums album = new Albums("Album 1","Artist 1");
        album.addNewSong("Album 1- Song 1", 23.23);
        album.addNewSong("Album 1- Song 2", 33.23);
        album.addNewSong("Album 1- Song 3", 13.23);
        albums.add(album);

        album = new Albums("Album 2","Artist 2");
        album.addNewSong("Album 2- Song 1", 32.23);
        album.addNewSong("Album 2- Song 2", 21.23);
        album.addNewSong("Album 2- Song 3", 1.23);
        albums.add(album);

        albums.get(0).addToPlayList("Album 1- Song 1",playlist);
        albums.get(0).addToPlayList("Album 1- Song 2",playlist);
        albums.get(0).addToPlayList("Album 1- Song 3",playlist);
        albums.get(1).addToPlayList("Album 2- Song 1",playlist);
        albums.get(1).addToPlayList("Album 2- Song 2",playlist);
        albums.get(1).addToPlayList("Album 2- Song 3",playlist);

        printMenu();
        play(playlist);
        //printList(playlist);
    }

    private static void play(LinkedList<Songs> playList){
        boolean quit=false;
        boolean forward = true;
        ListIterator<Songs> listIterator = playList.listIterator();
        if (playList.size()==0){
            System.out.println("No Songs in playlist");
            return;
        }else {
            System.out.println("Now Playing " + listIterator.next().toString());
        }

        while (!quit){
            Scanner scanner = new Scanner(System.in);
            int action =scanner.nextInt();
            scanner.nextLine();

            switch(action){
                case 0:
                    System.out.println("Play list complete");
                    quit=true;
                    break;
                case 1:
                    if (!forward){
                        if (listIterator.hasNext()){
                            listIterator.next();
                        }
                        forward=true;
                    }
                    if (listIterator.hasNext()){
                        System.out.println("Now playing "+ listIterator.next().toString());
                    } else{
                        System.out.println("End of list");
                        forward=false;

                    }
                    break;
                case 2:
                    if (forward){
                        if (listIterator.hasPrevious()){
                            listIterator.previous();
                        }
                        forward=false;
                    }
                    if (listIterator.hasPrevious()){
                        System.out.println("Now playing "+ listIterator.previous().toString());
                    } else{
                        System.out.println("Begining of list");
                        forward=true;

                    }
                    break;
                case 3:
                    if (forward){
                        if (listIterator.hasPrevious()){
                            System.out.println("Now playing " + listIterator.previous());
                            forward=false;
                        } else {
                            System.out.println("At beginning of list");
                        }
                    }else {
                        if (listIterator.hasNext()){
                            System.out.println("Now playing " + listIterator.next());
                            forward=true;
                        } else {
                            System.out.println("At end of list");
                        }
                    }
                    break;
                case 4:
                    printList(playList);
                    break;
                case 5:
                    printMenu();
                    break;
                case 6:
                    if (playList.size()>0){
                        listIterator.remove();
                        if (listIterator.hasNext()){
                            System.out.println("Now playing " + listIterator.next());
                        } else if (listIterator.hasPrevious()) {
                            System.out.println("Now playing " + listIterator.previous());
                        }
                    }


            }

        }

    }

    private static void printMenu() {
        System.out.println("Available actions:\npress");
        System.out.println("0 - to quit\n" +
                "1 - to play next song\n" +
                "2 - to play previous song\n" +
                "3 - to replay the current song\n" +
                "4 - list songs in the playlist\n" +
                "5 - print available actions.\n" +
                "6 - delete current song from playlist");

    }
    private static void printList(LinkedList<Songs> playList){
        Iterator<Songs> i = playList.iterator();
        System.out.println("Songs in playlist\n");
        while (i.hasNext()){
            System.out.println( i.next().toString());
        }

    }



    private static void deleteCurrentSong(String songName, LinkedList<Songs> playList){



    }

}
