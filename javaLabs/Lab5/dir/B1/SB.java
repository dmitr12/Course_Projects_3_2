package B1;

import A1.FA;

public class SB{
    public int x;
    FA fa;

    public SB(int x, FA fa){
        this.x=x; this.fa=fa;
    }

    public void display(){
        System.out.println("Class name: "+this.getClass().getName()+", x: "+x);
    }
    public void MethFA(){
        System.out.print("Method of FA: ");
        fa.display();
    }
}
