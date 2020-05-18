package B1.B2;

public class SBB {
    public int x;

    public SBB(int x){
        this.x=x;
    }

    public void display(){
        System.out.println("Class name: "+this.getClass().getName()+", x: "+x);
    }
}
