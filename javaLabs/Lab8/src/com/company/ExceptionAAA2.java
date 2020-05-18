package com.company;

public class ExceptionAAA2 extends Exception{
    Integer number;

    public ExceptionAAA2(String msg, Integer number){
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
