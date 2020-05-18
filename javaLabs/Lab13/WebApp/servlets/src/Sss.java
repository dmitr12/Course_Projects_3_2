import javax.servlet.RequestDispatcher;
import javax.servlet.Servlet;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

public class Sss extends HttpServlet implements Servlet {

    protected void service(HttpServletRequest rq, HttpServletResponse rs) throws ServletException, IOException {
        if(rq.getMethod()=="POST"){
            System.out.println("SSS:post");
            //Переопределение
            RequestDispatcher rd=rq.getRequestDispatcher("/GoGgg");
            rd.forward(rq,rs);
            PrintWriter pw=rs.getWriter();
            pw.print("SSS post<br>");
            pw.print("firstname: "+rq.getParameter("firstname")+"<br>");
            pw.print("lastname: "+rq.getParameter("lastname")+"<br>");
            pw.close();
        }
        super.service(rq,rs);
    }

    protected void doGet(HttpServletRequest rq, HttpServletResponse rs) throws IOException, ServletException {
        System.out.println("SSS:get");
        //Переопределение
        RequestDispatcher rd=null;
        PrintWriter pw=rs.getWriter();
        pw.print("SSS get<br>");
        //На сервлет
        rd=rq.getRequestDispatcher("/GoGgg?a=parameterA&b=parameterB");
        //На страницу
        //rd=rq.getRequestDispatcher(("/html/hello.html"));
        rd.forward(rq,rs);
        //Переадреcация
        //переадресация на сервлет
        //rs.sendRedirect("http://localhost:8080/WebApp/GoGgg?a=parameterA&b=parameterB");
        //переадресация на страницу
        //rs.sendRedirect("http://localhost:8080/WebApp/html/hello.html");
    }
}
