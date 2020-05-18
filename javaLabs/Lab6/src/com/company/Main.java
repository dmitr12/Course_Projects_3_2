package com.company;

import A1.A2.A3.AAA;
import java.util.Date;

public class Main {

    public static void main(String[] args) {
        AAA aaa=new AAA();
        aaa.setSurname("Lapitskiy");
        aaa.setFathername("Aleksandrovich");
        aaa.setName("Kirill");
        aaa.setBirthday(2000,6,13);
        aaa.setFirstDate(new Date(2017,8,28));
        aaa.setUniversity("BSTU");
        System.out.println(aaa.getSurname());
        System.out.println(aaa.getName());
        System.out.println(aaa.getFathername());
        System.out.println(Format(aaa.getBirthday()));
        System.out.println(aaa.getUniversity());
        System.out.println(Format(aaa.getFirstDate()));
    }

    static String Format(Date d)
    {
        return d.getDay()+"."+d.getMonth()+"."+d.getYear();
    }
}
