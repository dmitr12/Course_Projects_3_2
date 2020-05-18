package A1;

import B1.B2.FBB;

public class SA extends FBB {
    int x;
    public SA(int x){super(x);this.x=x;}

    public void Check(){
        System.out.print("Inherited method: ");
        super.display();
    }
}
