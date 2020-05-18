import org.apache.commons.httpclient.HttpClient;
import org.apache.commons.httpclient.HttpState;
import org.apache.commons.httpclient.HttpStatus;
import org.apache.commons.httpclient.NameValuePair;
import org.apache.commons.httpclient.methods.GetMethod;
import org.apache.commons.httpclient.methods.PostMethod;

import javax.servlet.Servlet;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class Sss2 extends HttpServlet implements Servlet {

    protected void service(HttpServletRequest rq, HttpServletResponse rs) throws ServletException, IOException {
        super.service(rq,rs);
    }

    protected void doGet(HttpServletRequest rq, HttpServletResponse rs) throws IOException {
        System.out.println("SSS2:get");
        PrintWriter pw=rs.getWriter();
        HttpClient client=new HttpClient();
        //Get запрос
        GetMethod gm=new GetMethod("http://localhost:8080"+rq.getContextPath()+"/GoGgg2?a=paramA&b=paramB");
        client.executeMethod(gm);
        rs.setContentType("text/html");
        pw.println(gm.getResponseBodyAsString());
        pw.close();
        //Post запрос
//        PostMethod pm=new PostMethod("http://localhost:8080"+rq.getContextPath()+"/GoGgg2?press=Submit");
//        NameValuePair[] params={
//                new NameValuePair("firstname","aaa"),
//                new NameValuePair("lastname","bbb")
//        };
//        pm.addParameters(params);
//        client.executeMethod(pm);
//        if(pm.getStatusCode()== HttpStatus.SC_OK) {
//            pw.println(pm.getResponseBodyAsString());
//        }
//        else{
//            System.out.println("Sss:service:getStatusCode()="+ pm.getStatusCode());
//        }
    }
}
