package com.company;

import java.util.Scanner;

public class Main {
    private static Scanner scanner= new Scanner(System.in);
    private static MobilePhone mobilephone = new MobilePhone("586 244 8503");

    public static void main(String[] args) {

        boolean quit=false;
        startPhone();
        printActions();
        while (!quit){
            System.out.println("\nEnter action: (6 to show available actions)");
            int action = scanner.nextInt();
            scanner.nextLine();

            switch (action){
                case 0:
                    System.out.println("\nShutting down...");
                    quit=true;
                    break;

                case 1:
                    mobilephone.printContacts();
                    break;

                case 2:
                    addNewContact();
                    break;

                case 3:
                    updateContact();
                    break;

                case 4:
                    removeContact();
                    break;

                case 5:
                    queryContact();
                    break;

                case 6:
                    printActions();
                    break;
            }
        }

    }

    private static void startPhone(){
        System.out.println("Starting phone....");
    }

    private static void printActions(){
        System.out.println("\nAvailable actions:\npress");
        System.out.println("0  -  to shut down\n " +
                "           1  -  to print contacts\n" +
                "           2  -  to add new contact\n" +
                "           3  -  to update existing contact\n" +
                "           4  -  to remove an exisiting contact" +
                "           5  -  query if existing contact exists\n" +
                "           6  -  to print list of availabe actions)");
        System.out.println("Choose your action: ");

    }

    private static void addNewContact(){
        System.out.println("Enter new contact name:");
        String name=scanner.nextLine();
        System.out.println("Enter new contact phone:");
        String phone=scanner.nextLine();
        //Does not create a new object or instanct of the Contacts class
        //beacuse .createContact is a static method in the class we can do the below
        //create a new contact record without having to create a new contact object
        Contacts newContact = Contacts.createContact(name,phone);
        if (mobilephone.addNewContact(newContact)){
            System.out.println("New contact added:");
        } else {
            System.out.println("Cannot add, already in list.");
        }
    }

    private static void updateContact(){
        System.out.println("Enter namne of contact to update");
        String name = scanner.nextLine();
        Contacts existingContact = mobilephone.queryContact(name);
        if (existingContact == null){
            System.out.println("Contact not found");
            return;
        }
        System.out.println("Enter new contact name");
        String newName = scanner.nextLine();
        System.out.println("Enter new contact phone");
        String newPhone = scanner.nextLine();
        Contacts newContact = Contacts.createContact(newName, newPhone);
        if (mobilephone.updateContact(existingContact,newContact)){
            System.out.println("Updated record");
        } else {
            System.out.println("Error");
        }


    }

    private static void removeContact() {
        System.out.println("Enter namne of contact to remove");
        String name = scanner.nextLine();
        Contacts existingContact = mobilephone.queryContact(name);
        if (existingContact == null) {
            System.out.println("Contact not found");
            return;
        }
        mobilephone.removeContact(existingContact);
        if (mobilephone.removeContact(existingContact)){
            System.out.println("Deleted record");
        } else {
            System.out.println("Error");
        }
    }

    private static void queryContact() {
        System.out.println("Enter namne of contact");
        String name = scanner.nextLine();
        Contacts existingContact = mobilephone.queryContact(name);
        if (existingContact == null) {
            System.out.println("Contact not found");
            return;
        }
        System.out.println("Name:" + existingContact.getContactName() + "\nPhone: "+ existingContact.getContactPhone());
    }
}
