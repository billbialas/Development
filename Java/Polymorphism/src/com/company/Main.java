package com.company;

public class Main {

    public static void main(String[] args) {
       Car car = new Car(8,"Base Car");
        System.out.println(car.startEngine());
        System.out.println(car.accelerate());
        System.out.println(car.brake());

        Mitshubishi mitshubishi = new Mitshubishi(6,"Oulander vrx 4wd");
        System.out.println(mitshubishi.startEngine());
        System.out.println(mitshubishi.accelerate());
        System.out.println(mitshubishi.brake());

        Ford ford = new Ford(8,"Ford Mustang");
        System.out.println(ford.startEngine());
        System.out.println(ford.accelerate());
        System.out.println(ford.brake());
    }
}

class Car {
    private boolean engine;
    private int cylinders;
    private int doors;
    private int wheels;
    private String name;

    public Car(int cylinders, String name) {
        this.cylinders = cylinders;
        this.name = name;
        this.wheels = 4;
        this.engine = true;

    }

    public int getCylinders() {
        return cylinders;
    }

    public int getDoors() {
        return doors;
    }

    public String getName() {
        return name;
    }

    public String startEngine() {
        return "Car -> startEngine()";
    }

    public String accelerate() {
        return "Car -> accelerate()";
    }

    public String brake() {
        return "Car -> brake()";


    }
}

class Mitshubishi extends Car {
    public Mitshubishi(int cylinders, String name) {
        super(cylinders, name);
    }

    @Override
    public String startEngine() {
        return "Mitshubishi -> startEngine()";
    }

    @Override
    public String accelerate() {
        return "Mitshubishi -> accelerate()";
    }

    @Override
    public String brake() {
        return "Mitshubishi -> brake()";
    }


}