package com.company;

import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.util.StringTokenizer;

public class Main {

    public static void main(String[] args) {

        Player bill = new Player("Bill", 100,120);
        System.out.println(bill.toString());

        bill.setHitPoints(50);
        System.out.println(bill);
        bill.setWeapon("Big Ass Gun");
        saveObect(bill);
//        loadObject(bill);
        System.out.println(bill);


        ISaveable monster = new Monster("Killer", 100, 100);
        System.out.println(monster);
        //example of casting because we declcared monster as ISavable
        System.out.println("Strength = " + ((Monster) monster).getStrength());
        saveObect(monster);


        //printvalues(readValues());
    }

    public static ArrayList<String> readValues(){
        ArrayList<String> values = new ArrayList<String>();

        Scanner scanner = new Scanner(System.in);
        boolean quit = false;
        int index=0;
        System.out.println("Choose\n" +
                            "1 to enter a string\n" +
                            "0 to quit");

        while (!quit){
            System.out.println("Choose an option: ");
            int choice = scanner.nextInt();
            scanner.nextLine();
            switch (choice){
                case 0:
                    quit=true;
                    break;
                case 1:
                    System.out.println("Enter a string: ");
                    String stringInput = scanner.nextLine();
                    values.add(index, stringInput);
                    index++;
                    break;
            }

        }
        return values;
    }

    public static void saveObect(ISaveable objectToSave){
        for (int i=0;i<objectToSave.write().size();i++){
            System.out.println("Saving  "+objectToSave.write().get(i) + " to storage device");
        }
    }

    public static void loadObject(ISaveable objectToLoad){
        List<String> values = readValues();
        objectToLoad.read(values);
    }

    public static void printvalues(ArrayList<String> prmvalues){
        ArrayList<String> values = new ArrayList<String>();
        values = prmvalues;
        for (int i=0;i<values.size();i++){
            System.out.println("Index: " + i + " value stored: " + values.get(i).toString());
        }

    }
}
