import org.apache.commons.httpclient.HttpClient;
import org.apache.commons.httpclient.methods.GetMethod;

import javax.servlet.Servlet;
import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class Sss extends HttpServlet implements Servlet {
    protected void service(HttpServletRequest rq, HttpServletResponse rs) throws ServletException, IOException {
        super.service(rq,rs);
    }

    protected void doGet(HttpServletRequest rq, HttpServletResponse rs) throws IOException {
        String urln=rq.getParameter("urln");
        HttpClient hc=new HttpClient();
        GetMethod gm;
        ServletContext sc=getServletContext();
        String uri=sc.getInitParameter("URL"+urln);
        PrintWriter pw=rs.getWriter();
        if(uri==null){
            pw.println("parameter URL"+urln+" not found");
            pw.close();
        }
        else{
            hc.executeMethod(gm=new GetMethod(uri));
            pw.println(gm.getResponseBodyAsString());
            pw.close();
        }
    }
}
