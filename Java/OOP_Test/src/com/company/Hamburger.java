package com.company;

import com.sun.org.apache.xpath.internal.SourceTree;

/**
 * Created by wbialas on 2/27/2016.
 */
public class Hamburger {
    private String name;
    private String bunType;
    private String meat;
    private double price;
    private double item1Price;
    private double item2Price;
    private double item3Price;
    private double item4Price;
    private String item1Name;
    private String item2Name;
    private String item3Name;
    private String item4Name;

    public Hamburger(String name, String bunType, String meat, double price) {
        this.name = name;
        this.bunType = bunType;
        this.meat = meat;
        this.price = price;
    }

    public void addItem1(String name, double price){
        this.item1Name=name;
        this.item1Price=price;
    }
    public void addItem2(String name, double price){
        this.item2Name=name;
        this.item2Price=price;
    }
    public void addItem3(String name, double price){
        this.item3Name=name;
        this.item3Price=price;
    }
    public void addItem4(String name, double price){
        this.item4Name=name;
        this.item4Price=price;
    }

    public double itemizeHamburger(){
        double hamburgerPrice = this.price;
        System.out.println(this.name + " burger \n with " + this.meat + " \n on " + this.bunType+ " \n price is "+ hamburgerPrice);

        if (this.item1Name != null){
            hamburgerPrice += this.item1Price;
            System.out.println("with " + this.item1Name+" @ "+ this.item1Price);
        }
        if (this.item2Name != null){
            hamburgerPrice += this.item2Price;
            System.out.println("with " + this.item2Name+" @ "+ this.item2Price);
        }
        if (this.item3Name != null){
            hamburgerPrice += this.item3Price;
            System.out.println("with " + this.item3Name+" @ "+ this.item3Price);
        }
        if (this.item4Name != null){
            hamburgerPrice += this.item4Price;
            System.out.println("with " + this.item4Name+" @ "+ this.item4Price);
        }


        return hamburgerPrice;


    }
}
