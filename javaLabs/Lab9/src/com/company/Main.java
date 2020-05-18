package com.company;

public class Main {

    public enum Planet{
        MERCURY,
        VENUS,
        EARTH,
        MARS,
        JUPITER,
        SATURN,
        URANUS,
        NEPTUN;
    }

    public enum Pl{
        MERCURY(3.302e+23, 2.439e+06),
        NEPTUN(1.024e+26, 2.477e+07);
        private final double mass;
        private final double radius;
        private final double gravity;
        private static  final double G=6.673000e-11;
        Pl(double mass, double radius){
            this.mass=mass;
            this.radius=radius;
            this.gravity=G*mass/(radius*radius);
        }
    }

    public enum MyEnum{
        ALICE(21), JOHN(56);
        private int age;
        MyEnum(int age){
            this.age=age;
        }
        public int getAge(){return age;}
    }

    public static void main(String[] args) {
        //Конструкторы
        System.out.println("Конструкторы");
        String s01=new String();
        System.out.println("s01.Length="+s01.length());
        char[] cs={'0','1','2','3','4','5','6','7'};
        String s02=new String(cs);
        System.out.println("s02.Length="+s02.length()+" s02="+s02);
        String s03=new String(cs,3,5);
        System.out.println("s03.Length="+s03.length()+" s03="+s03);
        String s04="01234567";
        System.out.println("s04.Length="+s04.length()+" s04="+s04);
        String s05=new String("01234567");
        System.out.println("s05.Length="+s05.length()+" s05="+s05);
        //Извлечение символов
        System.out.println("Извлечение символов");
        String s06=new String("01234567");
        for(int i=0;i<s06.length();i++)
            System.out.print(s06.charAt(i)+((i<s06.length()-1)?"-":"\n"));
        char[] cs1=new char[6-2];
        s06.getChars(2,6,cs1,0);
        for(int i=0;i<cs1.length;i++)
            System.out.print(cs1[i]+((i<cs1.length-1)?"-":"\n"));
        //Сравнение строк
        System.out.println("Сравнение строк");
        String s07=new String("01234567");
        String s08=new String("01234567");
        System.out.println("(s07==s08)="+(s07==s08));
        System.out.println("(s07==\"01234567\")="+(s07=="01234567"));
        System.out.println("s07.equals(s08)="+(s07.equals(s08)));
        String s09=s08;
        System.out.println("(s08==s09)="+(s08==s09));
        //Поиск символа или подстроки
        System.out.println("Поиск символа или подстроки");
        String s10=new String("Человека невозможно чему-нибудь научить, его нужно убедить");
        System.out.println(s10.indexOf('j'));
        System.out.println(s10.indexOf('о'));
        System.out.println(s10.lastIndexOf('о'));
        System.out.println(s10.indexOf('j'));
        System.out.println(s10.indexOf('о',5));
        System.out.println(s10.lastIndexOf('о', 5));
        System.out.println(s10.lastIndexOf('о', 14));
        //Извлесение подстроки
        System.out.println("Извлечение подстроки");
        String s11=new String("Лучший вид на этот город, если сесть в бомбардировщик");
        System.out.println(s11.substring(26));
        System.out.println(s11.substring(26,30));
        //Замена символов
        System.out.println("Замена символов");
        String s12=new String("Оффтопик-любое сетевое сообщение "+
                "выходящее за рамки ранее установленной темы");
        System.out.println(s12.replace('о','А'));
        System.out.println(s12.toUpperCase());
        System.out.println(s12.toLowerCase());
        //StringBuffer
        System.out.println("StringBuffer");
        StringBuffer s15=new StringBuffer(100);
        s15.append("В городе Сочи темные ночи");
        System.out.println("s15.length()="+s15.length());
        System.out.println("s15.capacity()="+s15.capacity());
        s15.insert(21,"и теплые ");
        System.out.println(s15);
        System.out.println("s15.length()="+s15.length());
        System.out.println("s15.capacity()="+s15.capacity());
        s15.delete(21,30);
        System.out.println(s15);
        s15.ensureCapacity(200);
        System.out.println("s15.capacity()="+s15.capacity());
        String s16=s15.toString();
        //Оболочки простых типов
        Byte x1=3;
        Short. x2=256;
        Integer x3=1000;
        Float x4=3.14f;
        Double x5=2.9e10;
        Long x6=20000000L;
        Character x7='f';
        Boolean x8=true;
        //Простые перечисления
        System.out.println("Простые перечисления");
        Planet planet=Planet.EARTH;
        switch(planet){
            case MERCURY:System.out.println("MERCURY");break;
            case JUPITER:System.out.println("JUPITER");break;
            default:System.out.println("default");
        }
        //Расширенные перечисления
        System.out.println("Расширенные перечисения");
        Pl pl=Pl.NEPTUN;
        System.out.println(pl.gravity);
        System.out.println(pl.mass);
        System.out.println(pl.radius);
        //MyEnum
        System.out.println("MyEnum");
        MyEnum me=MyEnum.JOHN;
        System.out.println(me.getAge());
    }
}
