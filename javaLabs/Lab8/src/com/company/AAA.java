package com.company;

public class AAA {

    Integer number;

    public AAA(Integer number) throws ExceptionAAA1 {
        if(number==null)
            throw new ExceptionAAA1("number is null", number);
        this.number=number;
    }

    public int methode(int n) throws ExceptionAAA2, ExceptionAAA3 {
        if(n==0)
            throw new ExceptionAAA2("number is 0",n);
        if(n<0)
            throw new ExceptionAAA3("number is less then 0", n);
        return n;
    }
}
