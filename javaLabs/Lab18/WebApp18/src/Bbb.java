import javax.servlet.Servlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Enumeration;

public class Bbb extends HttpServlet implements Servlet {
    protected void doPost(HttpServletRequest rq, HttpServletResponse rs) throws IOException {
        rs.setHeader("head1","one");
        rs.setHeader("head2","two");
        PrintWriter pw=rs.getWriter();
        pw.print("INPUT PARAMETERS: <br/>");
        pw.print("firstParam: "+rq.getParameter("firstParam")+"<br/>");
        pw.print("secondParam: "+rq.getParameter("secondParam")+"<br/>");
        pw.print("thirdParam: "+rq.getParameter("thirdParam")+"<br/>");
        pw.print("<br/>REQUEST HEADERS: <br/>");
        Enumeration enm=rq.getHeaderNames();
        String str;
        while(enm.hasMoreElements()){
            str=(String)enm.nextElement();
            pw.print(str+": "+rq.getHeader(str)+"<br/>");
        }
    }
}
