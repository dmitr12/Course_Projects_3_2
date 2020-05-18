package A1.A2;

public class FAA {
    public double x;

    public FAA(double x){
        this.x=x;
    }

    public void display(){
        System.out.println("Class name: "+this.getClass().getName()+", x: "+x);
    }
}



