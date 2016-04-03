package com.company;

/**
 * Created by wbialas on 2/27/2016.
 */
public class DeluxeBurger extends Hamburger {
    public DeluxeBurger() {
        super("Deluxe", "White", "Patty & Bacon", 5.60);
        super.addItem1("Chips", 2.75);
        super.addItem2("Drink",1.81 );
    }

    @Override
    public void addItem1(String name, double price) {
        System.out.println("Cannot add items to a Deluxe Burger");
    }

    @Override
    public void addItem2(String name, double price) {
        System.out.println("Cannot add items to a Deluxe Burger");
    }

    @Override
    public void addItem3(String name, double price) {
        System.out.println("Cannot add items to a Deluxe Burger");
    }

    @Override
    public void addItem4(String name, double price) {
        System.out.println("Cannot add items to a Deluxe Burger");
    }
}
