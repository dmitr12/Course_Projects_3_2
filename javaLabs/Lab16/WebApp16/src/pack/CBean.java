package pack;

public class CBean {
    protected String Value1=null;
    protected String Value2=null;
    protected String Value3=null;

    public CBean(){}

    public void setValue1(String Value1){
        this.Value1=Value1;
    }

    public void setValue2(String Value2){this.Value2=Value2; }

    public void setValue3(String Value3){
        this.Value3=Value3;
    }

    public String getValue1(){
        return Value1;
    }

    public String getValue2(){
        return Value2;
    }

    public String getValue3(){
        return Value3;
    }
}
