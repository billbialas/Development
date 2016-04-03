package com.company;

/**
 * Created by wbialas on 2/25/2016.
 */
public class Printer {
    private int tonerLevel;
    private int pagesPrinted;
    private boolean duplex;

    public Printer(int tonerLevel, int pagesPrinted, boolean duplex) {
        this.tonerLevel = tonerLevel;
        this.pagesPrinted = pagesPrinted;
        this.duplex = duplex;
    }

    public void printpage(int noofpages) {
        if (this.tonerLevel <= 10) {
            System.out.println("Toner low can not print");
        } else {
            if (this.duplex) {
                System.out.println("Duplex is set");
                pagesPrinted = pagesPrinted + (noofpages / 2);
            } else {
                System.out.println("Duplex is not set");
                pagesPrinted = pagesPrinted + noofpages;
            }
        }
        tonerLevel = tonerLevel - noofpages;
        System.out.println("Printing " + pagesPrinted + " pages");
        System.out.println("Toner level is now at " + tonerLevel+ " %");
    }

    public void filltoner(int filllevel) {
        if (this.tonerLevel <= 100) {
            this.tonerLevel += filllevel;
        } else {
            this.tonerLevel = 100;
        }
        System.out.println("Filling toner.  Toner now at " + tonerLevel + " %");
    }


    public int getTonerLevel() {
        return tonerLevel;
    }

    public int getPagesPrinted() {
        return pagesPrinted;
    }

    public boolean isDuplex() {
        return duplex;
    }
}
