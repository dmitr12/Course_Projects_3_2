package tglb;

import javax.servlet.jsp.JspException;
import javax.servlet.jsp.JspWriter;
import javax.servlet.jsp.tagext.TagSupport;
import java.io.IOException;

public class Sex extends TagSupport {
    static String in="<label>Sex: </label><input type=\"radio\" name=\"sex\" value=\"male\" checked=\"checked\">male" +
            "<input type=\"radio\" name=\"sex\" value=\"female\">female";

    public int doStartTag() throws JspException {
        JspWriter out=pageContext.getOut();
        try{
            out.println(in+"<p></p>");
        } catch (IOException e) {
            System.out.println("tglb.Sex: "+e);
        }
        return SKIP_BODY;
    }
}
