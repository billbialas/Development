package com.company;

public class Main {

    public static void main(String[] args) {
	    Customer customer = new Customer("Bill", 120.00);
        Customer anotherCustomer = new Customer("Ron",49.00);
//        Customer anotherCustomer = customer;
//        anotherCustomer.setAmount(49.00);
        System.out.println("Bal for customer " + customer.getAmount());
    }
}
