package com.company;

import java.util.ArrayList;
import java.util.StringTokenizer;

/**
 * Created by wbialas on 3/2/2016.
 */
public class Branch {
    private String branchName;
    private ArrayList<Customer> customer;

    public Branch(String branchName) {
        this.branchName = branchName;
        this.customer = new ArrayList<Customer>();
    }

    public ArrayList<Customer> getCustomer() {
        return customer;
    }

    public String getBranchName() {
        return branchName;
    }

    //So here we will add a new customer.  This will take a name and amount
    //Check if exists, if so return error
    //if not, customer is an arraylist
    public boolean addNewCustomer(String customerName, double initAmount) {
        if (findCustomer(customerName)== null) {
            this.customer.add(new Customer(customerName,initAmount));
            return true;
        }
        System.out.println("Customer is already a member");
        return false;
    }

    //Here we will create an instance of the Customer class
    //which means it can hold a cusomer and the Arraylist of all transactions
    //Use the find method to located and then the .addTransaction method defined in the Customer class
    public boolean addCustomerTransaction(String customerName, double amount){
        Customer existingCustomer=findCustomer(customerName);
        if (existingCustomer != null) {
            existingCustomer.addTransaction(amount);
            return true;
        }
        System.out.println("Customer is not a member");
        return false;
    }

    //Here loop through the arraulistof customers
    //first create a instacnce/object of the customer class (empty) Customer checkedCustomer
    //assign this.customer(i) to Customer checkedCustomer
    //if arrayname matches the pass parm name then return that inedexed object through the method
    //as the return parm is a class *Customer)
    private Customer findCustomer(String customerName){
        for (int i=0; i<this.customer.size();i++){
            Customer checkedCustomer = this.customer.get(i);
            if (checkedCustomer.getCustomerName().equals(customerName)){
                return checkedCustomer;
            }
        }
        return null;
    }
}
