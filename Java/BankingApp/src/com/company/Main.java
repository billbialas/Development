package com.company;

import java.util.ArrayList;
import java.util.Scanner;

public class Main {
    private static Scanner scanner= new Scanner(System.in);
    private static Bank bank = new Bank("Bialas Bank Of Trust");

    public static void main(String[] args) {
        bank.addBranch("Macomb");
        bank.addCustomer("Macomb","Bill",50.00);
        bank.addCustomer("Macomb","Stephanie",150.00);
        bank.addCustomer("Macomb","Ben",23.33);

        bank.addBranch("Sterling Heights");
        bank.addCustomer("Sterling Heights","Bill",220.00);
        bank.addCustomer("Sterling Heights","Maria",10.00);
        bank.addCustomerTransaction("Sterling Heights","Maria",20.00);
        bank.addCustomerTransaction("Sterling Heights","Maria",30.00);
        bank.addCustomer("Sterling Heights","Sam",80.00);

//        bank.listCustomers("Macomb",false);
//        bank.listCustomers("Macomb",true);
        bank.listCustomers("Sterling Heights",false);
        bank.listCustomers("Sterling Heights",true);

    }



}
