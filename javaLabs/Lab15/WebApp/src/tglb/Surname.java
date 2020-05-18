package tglb;

import javax.servlet.jsp.JspException;
import javax.servlet.jsp.JspWriter;
import javax.servlet.jsp.tagext.TagSupport;
import java.io.IOException;

public class Surname  extends TagSupport {
    static String in="<label>Surname: </label>"
            +"<input name=\"surname\" type=\"text\" width=\"150\" "
            +" maxlength=\"30\" ";
    public String value="";

    public String getValue(){
        return value;
    }
    public void setValue(String value){
        this.value=value;
    }

    public int doStartTag() throws JspException {
        JspWriter out = pageContext.getOut();
        try {
            out.print(in + (this.value.equals("") ? " " : "value =\"" + this.value + "\" />")+"<p></p>");
        } catch (IOException e) {
            System.out.println("tglb.Surname: " + e);
        }
        return SKIP_BODY;
    }
}