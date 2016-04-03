package com.company;

import java.util.ArrayList;
import java.util.StringTokenizer;

/**
 * Created by wbialas on 2/29/2016.
 */
public class MobilePhone {
    //Create an Array using the class 'Contacts'
    private ArrayList<Contacts> myContacts;
    private String myNumber;

    //Create constructor and init the array 'myContacts'
    //Best practice is to do this here instead of the above create statement
    public MobilePhone(String myNumber) {
        this.myNumber = myNumber;
        this.myContacts = new ArrayList<Contacts>();
    }

    //Method to add the contact
    //Check if it exists first using the 'getContactName' getter in the Contacts class
    //if not add using the .add method of an ArrayList
    //return tru/false as appropriate
    public boolean addNewContact(Contacts contact) {
        if (findContact(contact.getContactName()) >= 0) {
            System.out.println("Contact is already in list");
            return false;
        }

        myContacts.add(contact);
        return true;

    }

    public boolean updateContact(Contacts oldContact, Contacts newContact) {
        int foundPosition = findContact((oldContact));
        if (foundPosition < 0) {
            System.out.println(oldContact.getContactName() + ", was not found.");
            return false;
        }
        //update by passing the index and then the new value
        //this is using the .set method of the Arraylist class
        this.myContacts.set(foundPosition, newContact);
        System.out.println(oldContact.getContactName() + ", was replaced with " + newContact.getContactName());
        return true;
    }

    //this is the search method that is exposed while the findcontact is private beacuse it uses the index value instead
    // of the actual contact name
    public String queryContact(Contacts contact) {
        if (findContact(contact) >= 0) {
            return contact.getContactName();
        }
        return null;
    }

    public Contacts queryContact(String name){
        int position = findContact(name);
        if (position>=0) {
            return this.myContacts.get(position);
        }
        return null;
    }



    public boolean removeContact(Contacts contact) {
        int foundPosition = findContact(contact);
        if (foundPosition < 0) {
            System.out.println(contact.getContactName() + ", was not found");
            return false;
        }
        this.myContacts.remove(foundPosition);
        System.out.println(contact.getContactName() + ", was removed");
        return true;
    }


    //Create 2 findContact methods, 1 will overload
    //This find uses the Contacts class to receive the contact itself
    //used to get the index position using the .indexof method for Arraylist
    //myContacts is the Arraylist that I created in this class
    //indexof will return a 0+ if the contact exists; else it will return a -.  No need to manually do a return
    private int findContact(Contacts contact) {
        return this.myContacts.indexOf(contact);
    }

    //This find takes a contact name and loops through the Arraylist looking by name
    //myContacts is the Arraylist to this claas and we use the .size method to determine how big it is
    //we init a var called contact of the Contracts class and get the name

    private int findContact(String contactName) {
        for (int i = 0; i < this.myContacts.size(); i++) {
            //This creates a new object
            Contacts contact = this.myContacts.get(i);
            if (contact.getContactName().equals(contactName)) {
                return i;
            }
        }
        return -1;
    }

    public void printContacts(){
        System.out.println("Contacts list");
        for (int i=0;i<this.myContacts.size(); i++){
            System.out.println(i+1+". " +
                                this.myContacts.get(i).getContactName()+ "-> "+
                                this.myContacts.get(i).getContactPhone());
        }
    }


}
