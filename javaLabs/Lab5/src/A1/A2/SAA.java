package A1.A2;

public class SAA {
    public int x;

    public SAA(int x){
        this.x=x;
    }

    public void display(){
        System.out.println("Class name: "+this.getClass().getName()+", x: "+x);
    }
}
