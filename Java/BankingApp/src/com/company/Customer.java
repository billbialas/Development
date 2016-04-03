package com.company;

import java.util.ArrayList;

/**
 * Created by wbialas on 3/2/2016.
 */

    //So this customer class uses a String datatype to store one customer
    //and then an arraylist for each transaction
    //Constructor is used to init new customers only

    //Question: How do we tie the customer to all of his transactionsation.
    //We only have the single instance of the customer and arraylist of all transactions
    //in this class there is no index that ties them together, unless its the class itself or later
    // the new object customer
public class Customer{

    private String customerName;
    //this just defines but does not initialize the Arraylist
    ArrayList<Double> transaction;
    //or can be done this way (long hand)
    //ArrayList<Double> transaction = new ArrayList<Double>();

    //Constructor.  Example of how we can pass 2 unique data types to the constructor
    //then init the arraylist and store the initamount into the Arraylist
    public Customer(String customerName, double initAmount) {
        this.customerName = customerName;
        //Init the ArrayList
        this.transaction = new ArrayList<Double>();
        //call the method to add the initamount
        addTransaction(initAmount);
    }

    public void addTransaction (double amount){
        //this is an Arraylist so we can use the .add method to add the amount
        this.transaction.add(amount);
    }

    public String getCustomerName() {
        return customerName;
    }

    public ArrayList<Double> getTransaction() {
        return transaction;
    }

}
