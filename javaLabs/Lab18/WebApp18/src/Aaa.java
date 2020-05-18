import org.apache.commons.httpclient.Header;
import org.apache.commons.httpclient.HttpClient;
import org.apache.commons.httpclient.HttpStatus;
import org.apache.commons.httpclient.NameValuePair;
import org.apache.commons.httpclient.methods.PostMethod;

import javax.servlet.Servlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Collection;
import java.util.Enumeration;

public class Aaa extends HttpServlet implements Servlet{
    protected void service(HttpServletRequest rq, HttpServletResponse rs) throws IOException {
        HttpClient hc=new HttpClient();
        PostMethod pm=new PostMethod("http://localhost:8080"+rq.getContextPath()+"/Bbb");
        NameValuePair[] parms={
                new NameValuePair("firstParam","valueFirst"),
                new NameValuePair("secondParam","valueSecond"),
                new NameValuePair("thirdParam","valueThird")
        };
        pm.setRequestHeader("firstParam","valueFirst");
        pm.setRequestHeader("secondParam","valueSecond");
        pm.setRequestHeader("thirdParam","valueThird");
        pm.addParameters(parms);
        hc.executeMethod(pm);
        PrintWriter pw=rs.getWriter();
        if(pm.getStatusCode()== HttpStatus.SC_OK){
            Header[] enm=pm.getResponseHeaders();
            pw.println("RESPONSE HEADERS: <br/>");
            for(Header s:enm){
                pw.print(s.getName()+": "+s.getValue()+"<br/>");
            }
            pw.println("<br/>");
            pw.println(pm.getResponseBodyAsString());
        }
        else{
            System.out.println("BBB:POST "+pm.getStatusCode());
        }
    }
}