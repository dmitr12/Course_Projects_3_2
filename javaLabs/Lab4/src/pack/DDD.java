package pack;

public class DDD extends CCC {
    public int x;
    protected int y;

    public DDD() {
        super();
        this.x = 1;
        this.y = 1;
    }

    public DDD(int x, int y) {
        super(x, y);
        this.x = x;
        this.y = y;
    }

    public int sum() {
        return x + y + super.sum();
    }

    public int calc() {
        return x * y + super.x * super.y;
    }
}