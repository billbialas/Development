package com.company;

/**
 * Created by wbialas on 2/29/2016.
 */
public class Contacts {
    private String contactName;
    private String contactPhone;

    public Contacts(String contactName, String contactPhone) {

        this.contactName = contactName;
        this.contactPhone = contactPhone;
    }

    public String getContactName() {
        return contactName;
    }

    public String getContactPhone() {
        return contactPhone;
    }

    //Tale note on the static word here
    //this adherese this method to the class and is
    //One rule-of-thumb: ask yourself "does it make sense to call this method,
    // even if no Obj has been constructed yet?" If so, it should definitely be static.
    public static Contacts createContact(String name, String phoneNumber){
        return new Contacts(name,phoneNumber);
    }
}
