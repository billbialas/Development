package com.company;

import java.util.ArrayList;

/**
 * Created by wbialas on 3/2/2016.
 */
public class Bank {
    private String bankName;
    ArrayList<Branch> branches= new ArrayList<Branch>();

    public Bank(String bankName) {
        this.bankName = bankName;
        this.branches = new ArrayList<Branch>();
    }

    public boolean addBranch(String branchName){
        if (findBranch(branchName)==null){
            this.branches.add(new Branch(branchName));
            return true;
        }
        return false;
    }

    //Check if branch exists
    //if yes then call method to add new cusomter in the Branch class
    //We only need to validate that branch exists and not customer as customer is alredy validated
    //in the brach class
    public boolean addCustomer(String branchName, String customerName, double initAmount) {
        Branch branch = findBranch(branchName);
        if (branch != null) {
            return branch.addNewCustomer(customerName, initAmount);
        }
        return false;
    }
    public boolean addCustomerTransaction(String branchName, String customerName, double amount){
        Branch branch   =findBranch(branchName);
            if (branch != null){
                return branch.addCustomerTransaction(customerName,amount);
        }
        return false;

    }

    private Branch findBranch(String branchName){
        for (int i=0; i<this.branches.size();i++){
            Branch checkedBranch = this.branches.get(i);
            if (checkedBranch.getBranchName().equals(branchName)){
                return checkedBranch;
            }
        }
        return null;
    }

    public boolean listCustomers(String branchName, boolean showTransactions){
        Branch branch=findBranch(branchName);
        if (branch != null){
            System.out.println("Customers for branch " + branch.getBranchName());
            ArrayList<Customer> branchCustomers = branch.getCustomer();
            for (int i=0; i< branchCustomers.size(); i++){
                Customer branchCustomer = branchCustomers.get(i);
                System.out.println("Customer: " + branchCustomer.getCustomerName() + "[" + (i +1) + "]");
                if (showTransactions) {
                    System.out.println("Transactions");
                    ArrayList<Double> transactions = branchCustomer.getTransaction();
                    for (int j=0;j<transactions.size();j++){
                        System.out.println("[" + (j+1) + "] amount "+ transactions.get(j) );
                    }
                }
            }
            return true;
        }else {
            return false;
        }
    }

}
