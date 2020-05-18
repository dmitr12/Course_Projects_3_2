package com.company;

public class ExceptionAAA1 extends Exception {
    Integer number;

    public ExceptionAAA1(String msg, Integer number){
        super(msg);
        this.number=number;
    }

    public String getMessage(){
        return super.getMessage();
    }

    public void printStackTrace(){
        super.printStackTrace();
    }

    public String toString(){
        return "number is "+number;
    }
}
