import javax.servlet.RequestDispatcher;
import javax.servlet.Servlet;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class Ggg extends HttpServlet implements Servlet {

    protected void service(HttpServletRequest rq, HttpServletResponse rs) throws ServletException, IOException {

            super.service(rq,rs);
    }

    protected void doGet(HttpServletRequest rq, HttpServletResponse rs) throws IOException, ServletException {
        System.out.println("GGG:get");
        //Двойное переопределение
        /*RequestDispatcher rd=rq.getRequestDispatcher("/html/hello.html");
        rd.forward(rq,rs);*/
        //Двойная переадресация
        //rs.sendRedirect("http://localhost:8080/WebApp/html/hello.html");
        PrintWriter pw=rs.getWriter();
        pw.println("GGG get<br>");
        pw.println("a: "+rq.getParameter("a")+"<br>");
        pw.println("b: "+rq.getParameter("b")+"<br>");
        pw.close();

    }

    protected void doPost(HttpServletRequest rq, HttpServletResponse rs) throws IOException, ServletException {
        System.out.println("GGG:post");
        PrintWriter pw=rs.getWriter();
        pw.print("GGG post<br>");
        pw.print("firstname: "+rq.getParameter("firstname")+"<br>");
        pw.print("lastname: "+rq.getParameter("lastname")+"<br>");
        pw.close();
    }
}
