import javax.servlet.Servlet;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class Ttt extends HttpServlet implements Servlet {
    protected void service(HttpServletRequest rq, HttpServletResponse rs) throws ServletException, IOException {
        super.service(rq,rs);
    }

    protected void doPost(HttpServletRequest rq, HttpServletResponse rs) throws IOException {
        String surname=rq.getParameter("surname");
        String lastname=rq.getParameter("lastname");
        String sex=rq.getParameter("sex");
        rs.setContentType("text/html");
        PrintWriter pw=rs.getWriter();
        pw.println("Surname: "+surname+"<br>Lastname: "+lastname+"<br>Sex: "+sex);
        pw.close();
    }
}
