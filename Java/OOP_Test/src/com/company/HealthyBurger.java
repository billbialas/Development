package com.company;

/**
 * Created by wbialas on 2/27/2016.
 */
public class HealthyBurger extends Hamburger {
    private double healthy1Price;
    private double healthy2Price;
    private String healthy1Name;
    private String healthy2Name;

    public HealthyBurger(String meat, double price) {
        super("Healthy", "Whole Grain", meat, price);
    }

    public void addHealthyItem1(String name, double price) {
        this.healthy1Name = name;
        this.healthy1Price = price;
    }

    public void addHealthyItem2(String name, double price) {
        this.healthy2Name = name;
        this.healthy2Price = price;

    }

    @Override
    public double itemizeHamburger() {
        double hamburgerPrice = super.itemizeHamburger();

        if (this.healthy1Name != null){
            hamburgerPrice += this.healthy1Price;
            System.out.println("with " + this.healthy1Name+" @ "+ this.healthy1Price);
        }
        if (this.healthy2Name != null){
            hamburgerPrice += this.healthy2Price;
            System.out.println("with " + this.healthy2Name+" @ "+ this.healthy2Price);
        }
        return hamburgerPrice;
    }
}
