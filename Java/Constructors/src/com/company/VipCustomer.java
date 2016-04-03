package com.company;

/**
 * Created by wbialas on 2/24/2016.
 */
public class VipCustomer {
    private String name;
    private double creditLimit;
    private String emailAddress;

    public VipCustomer (){
        this("Bialas", 5000.00, "myemail@yaioo.com");
    }

    public VipCustomer (double creditlimit, String emailAddress){
        this("Bialas", creditlimit,  emailAddress);

    }

    public VipCustomer (String name, double creditlimit, String emailAddress){
        this.name = name;
        this.creditLimit= creditlimit;
        this.emailAddress = emailAddress;

    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public double getCreditLimit() {
        return creditLimit;
    }

    public void setCreditLimit(double creditLimit) {
        this.creditLimit = creditLimit;
    }

    public String getEmailAddress() {
        return emailAddress;
    }

    public void setEmailAddress(String emailAddress) {
        this.emailAddress = emailAddress;
    }
}
