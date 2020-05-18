import javax.servlet.Servlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class Afternoon extends HttpServlet implements Servlet {
    public Afternoon(){
        super();
    }
    protected void service(HttpServletRequest rq, HttpServletResponse rs) throws IOException {
        rs.setContentType("text/html");
        PrintWriter pw=rs.getWriter();
        pw.println("Good afternoon");
        pw.flush();
    }
}
