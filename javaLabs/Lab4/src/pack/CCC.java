package pack;

public class CCC {
    public int x;
    protected int y;

    public CCC() {
        x = 0;
        y = 0;
    }

    public CCC(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public int sum() {
        return x + y;
    }

    public static int ssub(int x, int y) {
        return x - y;
    }
}
