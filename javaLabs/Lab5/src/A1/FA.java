package A1;

public class FA {
    public int x;

    public FA(int x){
        this.x=x;
    }

    public void display(){
        System.out.println("Class name: "+this.getClass().getName()+", x: "+x);
    }
}

