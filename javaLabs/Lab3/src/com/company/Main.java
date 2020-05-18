package com.company;

import pack.StaticClass;

public class Main {

    public static void main(String[] args) {
        System.out.println("Задание 1.");
        int[] xx=new int[10];
        for(int i=0;i<xx.length;i++)
        {
            xx[i]=i;
            System.out.print(xx[i]+" ");
        }
        System.out.println("\nЗадание 2.");
        int[][] yy=new int[5][4];
        for(int i=0;i<yy.length;i++)
        {
            for(int j=0;j<yy[i].length;j++)
            {
                yy[i][j]=i+j;
                System.out.print(yy[i][j]+" ");
            }
            System.out.println();
        }
        System.out.println("Задание 3.");
        int y=2, z=8, p=-5;
        System.out.println("p>>>2="+(p>>>2)+", z<<=2 = "+(z<<=2));
        System.out.println("y^=5 = "+(y^=5));
        System.out.println("y&z="+(y&z));
        System.out.println("~y="+(~y));
        System.out.println("Задание 4.");
        int a=4, b=5;
        boolean ter=(a>4||b>4)&&b>4?true:false;
        System.out.println(ter);
        System.out.println("Задание 5.");
        if(b>a)
            System.out.println(b+">"+a);
        else
            System.out.println(b+"<"+a);
        for(int i:xx)
            System.out.print(i+" ");
        System.out.println();
        while(a>0)
        {
            System.out.print(a+" ");
            a--;
        }
        System.out.println();
        b=0;
        do{
            System.out.print(b+" ");
            b--;
        }while(b>0);
        System.out.println();
        int v=5;
        switch(v) {
            case 4:
                v++;
                break;
            case 5:
                v+=2;
                break;
            default:v+=5;
        }
        System.out.println("v="+v);
        System.out.println("Задание 6.");
        StaticClass.x=6;
        StaticClass.displayX();
    }
}
