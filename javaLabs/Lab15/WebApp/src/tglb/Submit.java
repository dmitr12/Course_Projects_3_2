package tglb;

import javax.servlet.jsp.JspException;
import javax.servlet.jsp.JspWriter;
import javax.servlet.jsp.tagext.TagSupport;
import java.io.IOException;

public class Submit extends TagSupport {
    static String in="<input type=\"submit\" name=\"press\" value=\"OK\" size=\"20\"/>" +
            "<input type=\"submit\" name=\"press\" value=\"Cancel\" size=\"20\" style=\"margin-left:20px;\"/>";

    public int doStartTag() throws JspException {
        JspWriter out=pageContext.getOut();
        try{
            out.println(in);
        } catch (IOException e) {
            System.out.println("tglb.Submit: "+e);
        }
        return SKIP_BODY;
    }
}
