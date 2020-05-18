package B1;

public class FB {
    public int x;

    public FB(int x){
        this.x=x;
    }

    public void display(){
        System.out.println("Class name: "+this.getClass().getName()+", x: "+x);
    }
}

