package com.company;

public class Main {

    public static void main(String[] args) {
        Hamburger hamburger=new Hamburger("Regular", "white", "patty", 2.75);
        //System.out.println("Total Cost is $" + hamburger.itemizeHamburger());
        // hamburger.addItem1("Cheese", 1.25);
        // hamburger.addItem2("Lettuce", .75);
        //hamburger.addItem3("Tomato", .50);
        // System.out.println("Total Cost is $" + hamburger.itemizeHamburger());

        HealthyBurger healthyBurger = new HealthyBurger("Turkey", 4.00);
        healthyBurger.addItem1("lettuce", .75);
        healthyBurger.addHealthyItem1("tufu", 1.75);
        System.out.println("Total Cost is $" + healthyBurger.itemizeHamburger());

        DeluxeBurger deluxeBurger=new DeluxeBurger();
        deluxeBurger.addItem1("lettuce",1.02);
        System.out.println("Total Cost is $" + deluxeBurger.itemizeHamburger());
    }
}
