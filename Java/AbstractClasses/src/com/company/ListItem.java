package com.company;

/**
 * Created by wbialas on 3/11/2016.
 *
 * this will be the base abstract class for simulating a linked list
 *
 */
public abstract class ListItem {

    //rightlink/leftlink are holders for repspective values
    //Note: they have been defined as datatypes of the class itself
    //Not sure I understand why this has to be
    //protected versus private because need to acces from concrete class
    protected ListItem rightlink = null;
    protected ListItem leftling = null;

    //defining value as an obnject makes assignment generic for classes that extaend from this one
    protected Object value;

    public ListItem(Object value) {
        this.value = value;
    }

    abstract ListItem next();
    abstract ListItem setNext(ListItem item);
    abstract ListItem previous();
    abstract ListItem setPrevious(ListItem item);

    //Compare items
    abstract int compareTo(ListItem item);

    //Again Object is used as the datatype to keep generic for definitaiton in later classes
    public Object getValue() {
        return value;
    }

    public void setValue(Object value) {
        this.value = value;
    }
}
