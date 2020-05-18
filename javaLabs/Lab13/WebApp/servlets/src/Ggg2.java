import javax.servlet.Servlet;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class Ggg2 extends HttpServlet implements Servlet {

    protected void service(HttpServletRequest rq, HttpServletResponse rs) throws ServletException, IOException {
        super.service(rq,rs);
    }

    protected void doGet(HttpServletRequest rq, HttpServletResponse rs) throws IOException {
        System.out.println("GGG2:get");
        rs.setContentType("text/html");
        PrintWriter pw=rs.getWriter();
        pw.println("response from GET method Ggg2: a="+rq.getParameter("a")+", b="+rq.getParameter("b"));
        pw.flush();
    }

    protected void doPost(HttpServletRequest rq, HttpServletResponse rs) throws IOException {
        System.out.println("GGG2:post");
        rs.setContentType("text/html");
        PrintWriter pw=rs.getWriter();
        pw.println("response from POST method Ggg2: firstname="+rq.getParameter("firstname")+", lastname="+rq.getParameter("lastname"));
        pw.flush();
    }
}