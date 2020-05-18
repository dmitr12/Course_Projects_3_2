public class Main {

    public static void main(String[] args) {
        int a1=5, b1=4;
        float a2=5.13f, b2=4.3f;
        double d=5.768, p=10.3213, a3=p, b3=d;
        System.out.println((Math.pow(a1, 2)-Math.pow(b1,2))+", "+
                (Math.pow(a2, 3)-Math.pow(b2, 3))+", "+
                (Math.pow(a3, 4)-Math.pow(b3, 4)));

        char a='a', b='b';
        System.out.printf("a-b=%d, a+b=%d", a-b,a+b);
    }
}