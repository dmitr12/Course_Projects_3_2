package com.company;

public class Main {

    public static void main(String[] args) {
        try{
            AAA aaa=new AAA(9);
            aaa.methode(-5);
        }
        catch (ExceptionAAA1 ex){
            System.out.println(ex.getMessage());
            ex.printStackTrace();
            ex.toString();
        }
        catch (ExceptionAAA2 ex){
            System.out.println(ex.getMessage());
            ex.printStackTrace();
            ex.toString();
        }
        catch (ExceptionAAA3 ex){
            System.out.println(ex.getMessage());
            ex.printStackTrace();
            ex.toString();
        }
    }
}
