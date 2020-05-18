package com.company;

import pack.*;

import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        System.out.println("Задание 1.");
        int x, y;
        CCC c1 = new CCC();
        CCC c2 = new CCC(4, 5);
        DDD d1 = new DDD();
        DDD d2 = new DDD(3, 7);
        System.out.println("c1.sum()=" + c1.sum());
        System.out.println("c2.sum()=" + c2.sum());
        System.out.println("Введите параметр x");
        Scanner scan = new Scanner(System.in);
        x = scan.nextInt();
        System.out.println("Введите параметр y");
        y = scan.nextInt();
        System.out.println("Выполнение метода CCC.ssub(x,y)=" + CCC.ssub(x, y));
        System.out.println("Задание 2.");
        System.out.println("d1.sum()=" + d1.sum());
        System.out.println("d2.sum()=" + d2.sum());
        System.out.println("d2.calc()=" + d2.calc());
        System.out.println("DDD.ssub(x,y)=" + DDD.ssub(x, y));
    }
}
